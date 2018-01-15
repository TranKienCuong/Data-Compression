using System;
using System.Collections.Generic;
using System.Drawing;
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

        /// <summary>
        /// roll out an image to string, example: [(1,2,3),(4,5,6),(7,8,9),(1,2,3)] -> 123456789123
        /// </summary>
        /// <param name="image">24-bit bitmap image</param>
        /// <returns></returns>
        public static string ConvertImageToString(Bitmap image)
        {
            string result = "";
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color color = image.GetPixel(i, j);
                    result += color.R.ToString();
                    result += color.G.ToString();
                    result += color.B.ToString();
                }
            }
            return result;
        }

        /// <summary>
        /// convert one string to 24-bit bitmap image, example: (147125823693, 2, 2) -> [(1,2,3),(4,5,6),(7,8,9),(1,2,3)]
        /// </summary>
        /// <param name="data">input string</param>
        /// <param name="width">image width</param>
        /// <param name="height">image height</param>
        /// <returns></returns>
        public static Bitmap ConvertStringToImage(string data, int width, int height)
        {
            Bitmap image = new Bitmap(width, height);
            int k = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int r = data[k++];
                    int g = data[k++];
                    int b = data[k++];
                    Color color = Color.FromArgb(r, g, b);
                    image.SetPixel(i, j, color);
                }
            }
            return image;
        }
    }
}
