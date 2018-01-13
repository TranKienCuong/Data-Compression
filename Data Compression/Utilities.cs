using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public static class Utilities
    {
        /// <summary>
        /// example: ("001001000", 3) -> [1, 1, 0]; ("001001000", 4) -> [2, 4]
        /// </summary>
        /// <param name="binaryString">binary string</param>
        /// <param name="bitLength">number of bits represent one integer</param>
        /// <returns></returns>
        public static List<int> ConvertBinaryStringToListInt(string binaryString, int bitLength = 8)
        {
            List<int> result = new List<int>();
            for (int i = 0; i <= binaryString.Length - bitLength; i += bitLength)
            {
                int x = ConvertBinaryStringToInteger(binaryString.Substring(i, bitLength));
                result.Add(x);
            }
            return result;
        }

        /// <summary>
        /// example: ([1, 1], 4) -> "00010001"; ([1, 2], 4) -> "00010010"
        /// </summary>
        /// <param name="ints">list of integers</param>
        /// <param name="bitLength">number of bits represent one integer</param>
        /// <returns></returns>
        public static string ConvertListIntToBinaryString(List<int> ints, int bitLength = 8)
        {
            string result = "";
            foreach (int i in ints)
            {
                result += ConvertIntegerToBinaryString(i, bitLength);
            }
            return result;
        }

        /// <summary>
        /// example: "0010" -> 2; "111" -> 7
        /// </summary>
        /// <param name="input">binary string</param>
        /// <returns></returns>
        public static int ConvertBinaryStringToInteger(string input)
        {
            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '1')
                {
                    sum += (int)Math.Pow(2, input.Length - i - 1);
                }
            }
            return sum;
        }

        /// <summary>
        /// example: (10, 4) -> "1010"; (10, 5) -> "01010"
        /// </summary>
        /// <param name="input">integer</param>
        /// <param name="bitLength">number of bits represent the integer</param>
        /// <returns></returns>
        public static string ConvertIntegerToBinaryString(int input, int bitLength = 8)
        {
            string binaryString = "";
            for (int i = 0; i < bitLength; i++)
            {
                binaryString = (input % 2).ToString() + binaryString;
                input /= 2;
            }
            return binaryString;
        }
    }
}
