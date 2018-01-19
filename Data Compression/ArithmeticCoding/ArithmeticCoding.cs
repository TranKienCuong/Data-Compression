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
                Tuple<double, double> rangeSymbol = FrequencyOfCharacters.RANGES[symbol];
                double lowSymbol = rangeSymbol.Item1;
                double highSymbol = rangeSymbol.Item2;
                int k = Math.Max(GetFractionalDigits(lowSymbol), GetFractionalDigits(highSymbol));
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

            return Utilities.ConvertStringToBytes(result.ToString());
        }

        string GeneratingCodewords(BigInteger low, BigInteger high)
        {
            StringBuilder result = new StringBuilder("");

            return result.ToString();
        }

        int GetFractionalDigits(double number)
        {
            string str = number.ToString();
            return str.Substring(str.IndexOf(".") + 1).Length;
        }
    }
}
