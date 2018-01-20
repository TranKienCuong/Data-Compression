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
            for (int i = 0; i < data.Length; i++)
            {
                byte symbol = data[i];
                Tuple<double, double> rangeSymbol = Characters.RANGES[symbol];
                double lowSymbol = rangeSymbol.Item1;
                double highSymbol = rangeSymbol.Item2;
                int k = Math.Max(GetFractionalDigitsLength(lowSymbol), GetFractionalDigitsLength(highSymbol));
                double pow = Math.Pow(10, k);
                BigInteger iPow = new BigInteger(pow);
                BigInteger iLow = low * iPow;
                BigInteger iLowSymbol = new BigInteger(lowSymbol * pow);
                BigInteger iHighSymbol = new BigInteger(highSymbol * pow);
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
                binaryString.Remove(binaryString.Length - 1);
            int n = binaryString.Length;
            BigInteger codeValue = new BigInteger(0);
            BigInteger pow = new BigInteger(Math.Pow(10, n));
            for (int i = 0; i < n; i++)
                codeValue = codeValue + pow / new BigInteger(Math.Pow(2, i + 1));

            BigInteger terminator = new BigInteger();
            while (codeValue < terminator)
            {

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
                    codeValue = codeValue + new BigInteger(Math.Pow(5, k)) * new BigInteger(Math.Pow(10, d - k));
                }
                else
                {
                    int i = k - d;
                    BigInteger pow = new BigInteger(Math.Pow(10, i));
                    codeValue = codeValue * pow + new BigInteger(Math.Pow(5, k));
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
            BigInteger pow = new BigInteger(Math.Pow(10, d - 5));
            while (begin <= end)
            {
                int mid = (begin + end) / 2;
                BigInteger low = new BigInteger((int)(Characters.RANGES[mid].Item1 * 100000));
                BigInteger high = new BigInteger((int)(Characters.RANGES[mid].Item2 * 100000));
                low *= pow; high *= pow;
                if (low <= codeValue && codeValue < high)
                    return mid;
                else if (codeValue < high)
                    end = mid - 1;
                else
                    begin = mid + 1;
            }
            return -1;
        }
    }
}
