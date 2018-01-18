﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            StringBuilder result = new StringBuilder("");
            foreach (int i in ints)
            {
                result.Append(ConvertIntegerToBinaryString(i, bitLength));
            }
            return result.ToString();
        }

        /// <summary>
        /// example: "0010" -> 2; "111" -> 7
        /// </summary>
        /// <param name="input">binary string</param>
        /// <returns></returns>
        public static int ConvertBinaryStringToInteger(string input)
        {
            return Convert.ToInt32(input, 2);
        }

        /// <summary>
        /// example: (10, 4) -> "1010"; (10, 5) -> "01010"
        /// </summary>
        /// <param name="input">integer</param>
        /// <param name="bitLength">number of bits represent the integer</param>
        /// <returns></returns>
        public static string ConvertIntegerToBinaryString(int input, int bitLength = 8)
        {
            return Convert.ToString(input, 2).PadLeft(bitLength, '0');
        }

        /// <summary>
        /// convert one string to a 2D array of colors, example: (147125823693, 2, 2) -> [[(1,2,3),(4,5,6)],[(7,8,9),(1,2,3)]]
        /// </summary>
        /// <param name="data">input string</param>
        /// <param name="width">width of array</param>
        /// <param name="height">height of array</param>
        /// <returns></returns>
        public static Color[,] ConvertStringToColorMatrix(string data, int width, int height)
        {
            Color[,] matrix = new Color[width, height];
            int k = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int r = data[k++];
                    int g = data[k++];
                    int b = data[k++];
                    Color color = Color.FromArgb(r, g, b);
                    matrix[i, j] = color;
                }
            }
            return matrix;
        }

        /// <summary>
        /// roll out a 2D array of colors to one string, example: [[(1,2,3),(4,5,6)],[(7,8,9),(1,2,3)]] -> 123456789123
        /// </summary>
        /// <param name="matrix">2D array of colors</param>
        /// <param name="width">width of array</param>
        /// <param name="height">height of array</param>
        /// <returns></returns>
        public static string ConvertColorMatrixToString(Color[,] matrix, int width, int height)
        {
            StringBuilder result = new StringBuilder("");
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color color = matrix[i, j];
                    result.Append(((char)color.R));
                    result.Append(((char)color.G));
                    result.Append(((char)color.B));
                }
            }
            return result.ToString();
        }
        
        /// <summary>
        /// convert a string to an array of bytes
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns></returns>
        public static byte[] ConvertStringToBytes(string input)
        {
            byte[] bytes = new byte[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                byte current = Convert.ToByte(input[i]);
                bytes[i] = current;
            }
            return bytes;
        }

        /// <summary>
        /// convert an array of bytes to a string
        /// </summary>
        /// <param name="input">array of bytes</param>
        /// <returns></returns>
        public static string ConvertBytesToString(byte[] input)
        {
            StringBuilder result = new StringBuilder("");
            for (int i = 0; i < input.Length; i++)
            {
                result.Append((char)input[i]);
            }
            return result.ToString();
        }
    }
}
