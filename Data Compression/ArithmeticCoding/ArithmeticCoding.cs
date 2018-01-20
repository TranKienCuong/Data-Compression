using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Data_Compression
{
    public class ArithmeticCoding
    {
        public byte[] Encode(byte[] data)
        {
            StringBuilder result = new StringBuilder("");
            BigInteger low = 0, high = 1, range = 1;
            for (int i = 0; i <= data.Length; i++)
            {
                Tuple<double, double> rangeSymbol;
                if (i != data.Length)
                {
                    byte symbol = data[i];
                    rangeSymbol = Characters.RANGES[symbol];
                }
                else
                    rangeSymbol = Characters.RANGES[256];
                double lowSymbol = rangeSymbol.Item1;
                double highSymbol = rangeSymbol.Item2;
                int k = Math.Max(GetFractionalDigitsLength(lowSymbol), GetFractionalDigitsLength(highSymbol));
                BigInteger iPow = BigInteger.Pow(10, k);
                BigInteger iLow = low * iPow;
                BigInteger iLowSymbol = Multiply(lowSymbol, iPow);
                BigInteger iHighSymbol = Multiply(highSymbol, iPow);
                low = iLow + range * iLowSymbol;
                high = iLow + range * iHighSymbol;
                range = high - low;
            }
            string binaryString = GeneratingCodewords(low, high);
            while (binaryString.Length % 8 != 0)
                binaryString += "0";
            List<int> ints = Utilities.ConvertBinaryStringToListInt(binaryString);
            foreach (int i in ints)
                result.Append((char)i);
            return Utilities.ConvertStringToBytes(result.ToString());
        }

        public byte[] Decode(byte[] data)
        {
            StringBuilder result = new StringBuilder("");
            List<int> ints = new List<int>();
            for (int i = 0; i < data.Length; i++)
                ints.Add(data[i]);
            string binaryString = Utilities.ConvertListIntToBinaryString(ints);
            while (binaryString.Last() == '0')
                binaryString = binaryString.Remove(binaryString.Length - 1);
            int n = binaryString.Length;
            BigInteger codeValue = new BigInteger(0);
            BigInteger pow = BigInteger.Pow(10, n);
            for (int i = 0; i < n; i++)
                if (binaryString[i] == '1')
                    codeValue = codeValue + pow / BigInteger.Pow(2, i + 1);

            int d = GetDigitsLength(codeValue);
            pow = BigInteger.Pow(10, d - 5);
            BigInteger terminator = 99939 * pow;
            while (codeValue < terminator)
            {
                int i = BinarySearch(codeValue);
                result.Append((char)i);
                double lowSymbol = Characters.RANGES[i].Item1;
                double highSymbol = Characters.RANGES[i].Item2;
                double rangeSymbol = highSymbol - lowSymbol;
                int k = GetFractionalDigitsLength(rangeSymbol);
                BigInteger low = new BigInteger((lowSymbol * 100000)) * pow;
                BigInteger high = new BigInteger((highSymbol * 100000)) * pow;
                BigInteger range = Multiply(rangeSymbol, BigInteger.Pow(10, k));
                BigInteger temp = (codeValue - low) * BigInteger.Pow(10, k);
                for (int j = 0; j < 100; j++)
                {
                    if (BigInteger.Remainder(temp, range) == 0)
                        break;
                    temp *= 10;
                }
                codeValue = temp / range;
                d = GetDigitsLength(codeValue);
                pow = BigInteger.Pow(10, d - 5);
                terminator = 99939 * pow;
            }
            return Utilities.ConvertStringToBytes(result.ToString());
        }

        int GetFractionalDigitsLength(double number)
        {
            string str = number.ToString();
            return str.Substring(str.IndexOf(".") + 1).Length;
        }

        int GetDigitsLength(BigInteger number)
        {
            return (int)Math.Floor(BigInteger.Log10(number) + 1);
        }

        string GeneratingCodewords(BigInteger low, BigInteger high)
        {
            StringBuilder result = new StringBuilder("");
            HashSet<int> indices = new HashSet<int>();
            BigInteger codeValue = new BigInteger(0);
            BigInteger oldValue = new BigInteger(0);
            int d = GetDigitsLength(low);
            int k = 1;
            while (codeValue < low)
            {
                indices.Add(k);
                if (k <= d)
                {
                    codeValue = codeValue + BigInteger.Pow(5, k) * BigInteger.Pow(10, d - k);
                }
                else
                {
                    int i = k - d;
                    BigInteger pow = BigInteger.Pow(10, i);
                    codeValue = codeValue * pow + BigInteger.Pow(5, k);
                    low = low * pow;
                    high = high * pow;
                    oldValue = oldValue * pow;
                    d = k;
                }
                if (codeValue >= high)
                {
                    indices.Remove(k);
                    codeValue = oldValue;
                }
                else
                    oldValue = codeValue;
                k++;
            }
            int n = k - 1;
            for (int i = 0; i < n; i++)
            {
                if (indices.Contains(i + 1))
                    result.Append("1");
                else
                    result.Append("0");
            }
            return result.ToString();
        }

        int BinarySearch(BigInteger codeValue)
        {
            int begin = 0, end = 255;
            int d = GetDigitsLength(codeValue);
            BigInteger pow = BigInteger.Pow(10, d - 5);
            while (begin <= end)
            {
                int mid = (begin + end) / 2;
                BigInteger low = new BigInteger((Characters.RANGES[mid].Item1 * 100000)) * pow;
                BigInteger high = new BigInteger((Characters.RANGES[mid].Item2 * 100000)) * pow;
                if (low <= codeValue && codeValue < high)
                    return mid;
                else if (codeValue < high)
                    end = mid - 1;
                else
                    begin = mid + 1;
            }
            return -1;
        }

        BigInteger Multiply(double d, BigInteger bi)
        {
            if (d == 0) return 0;
            if (d == 1) return bi;
            int k = GetFractionalDigitsLength(d);
            long pow = (long)Math.Pow(10, k);
            return bi * long.Parse(d.ToString().Split('.')[1]) / pow;
        }
    }
}
