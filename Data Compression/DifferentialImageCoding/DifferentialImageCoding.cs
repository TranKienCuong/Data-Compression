using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Data_Compression
{
    public class DifferentialImageCoding
    {
        const int BIAS = 128;

        public string Encode(Bitmap input)
        {
            StringBuilder result = new StringBuilder("");

            int width = input.Width;
            int height = input.Height;
            Color[,] output = new Color[width, height];
            List<int> unchangedBytes = new List<int>();

            BitmapData inputData = input.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, input.PixelFormat);

            // root pixel
            output[0, 0] = GetPixel(inputData, 0, 0);

            // [c3, c2]
            // [c1, c ]

            // P1 predictor: c1
            for (int i = 1; i < width; i++)
            {
                Color c1 = GetPixel(inputData, i - 1, 0);
                Color c = GetPixel(inputData, i, 0);
                int r = c.R - c1.R + BIAS;
                int g = c.G - c1.G + BIAS;
                int b = c.B - c1.B + BIAS;
                if (r < 0 || r > 255)
                {
                    r = c.R;
                    unchangedBytes.Add(i * 3);
                }
                if (g < 0 || g > 255)
                {
                    g = c.G;
                    unchangedBytes.Add(i * 3 + 1);
                }
                if (b < 0 || b > 255)
                {
                    b = c.B;
                    unchangedBytes.Add(i * 3 + 2);
                }
                output[i, 0] = Color.FromArgb(r, g, b);
            }
            // P2 predictor: c2
            for (int j = 1; j < height; j++)
            {
                Color c2 = GetPixel(inputData, 0, j - 1);
                Color c = GetPixel(inputData, 0, j);
                int r = c.R - c2.R + BIAS;
                int g = c.G - c2.G + BIAS;
                int b = c.B - c2.B + BIAS;
                if (r < 0 || r > 255)
                {
                    r = c.R;
                    unchangedBytes.Add(j * width * 3);
                }
                if (g < 0 || g > 255)
                {
                    g = c.G;
                    unchangedBytes.Add(j * width * 3 + 1);
                }
                if (b < 0 || b > 255)
                {
                    b = c.B;
                    unchangedBytes.Add(j * width * 3 + 2);
                }
                output[0, j] = Color.FromArgb(r, g, b);               
            }
            // P4 predictor: c1 + c2 - c3
            for (int i = 1; i < width; i++)
            {
                for (int j = 1; j < height; j++)
                {
                    Color c1 = GetPixel(inputData, i - 1, j);
                    Color c2 = GetPixel(inputData, i, j - 1);
                    Color c3 = GetPixel(inputData, i - 1, j - 1);
                    Color c = GetPixel(inputData, i, j);
                    int r = c.R - (c1.R + c2.R - c3.R) + BIAS;
                    int g = c.G - (c1.G + c2.G - c3.G) + BIAS;
                    int b = c.B - (c1.B + c2.B - c3.B) + BIAS;
                    if (r < 0 || r > 255)
                    {
                        r = c.R;
                        unchangedBytes.Add(j * width * 3 + i * 3);
                    }
                    if (g < 0 || g > 255)
                    {
                        g = c.G;
                        unchangedBytes.Add(j * width * 3 + i * 3 + 1);
                    }
                    if (b < 0 || b > 255)
                    {
                        b = c.B;
                        unchangedBytes.Add(j * width * 3 + i * 3 + 2);
                    }
                    output[i, j] = Color.FromArgb(r, g, b);                   
                }
            }
            input.UnlockBits(inputData);

            string binary = Utilities.ConvertIntegerToBinaryString(unchangedBytes.Count, 32);
            result.Append((char)Utilities.ConvertBinaryStringToInteger(binary.Substring(0, 8)));
            result.Append((char)Utilities.ConvertBinaryStringToInteger(binary.Substring(8, 8)));
            result.Append((char)Utilities.ConvertBinaryStringToInteger(binary.Substring(16, 8)));
            result.Append((char)Utilities.ConvertBinaryStringToInteger(binary.Substring(24, 8)));
            foreach (int i in unchangedBytes)
            {
                binary = Utilities.ConvertIntegerToBinaryString(i, 32);
                result.Append((char)Utilities.ConvertBinaryStringToInteger(binary.Substring(0, 8)));
                result.Append((char)Utilities.ConvertBinaryStringToInteger(binary.Substring(8, 8)));
                result.Append((char)Utilities.ConvertBinaryStringToInteger(binary.Substring(16, 8)));
                result.Append((char)Utilities.ConvertBinaryStringToInteger(binary.Substring(24, 8)));
            }
            result.Append(Utilities.ConvertColorMatrixToString(output, width, height));
            Console.WriteLine(unchangedBytes.Count);
            return result.ToString();
        }

        public Bitmap Decode(string data, int width, int height)
        {
            HashSet<int> unchangedBytes = new HashSet<int>();
            StringBuilder binary = new StringBuilder("");
            binary.Append(Utilities.ConvertIntegerToBinaryString(data[0], 8));
            binary.Append(Utilities.ConvertIntegerToBinaryString(data[1], 8));
            binary.Append(Utilities.ConvertIntegerToBinaryString(data[2], 8));
            binary.Append(Utilities.ConvertIntegerToBinaryString(data[3], 8));
            int unchangedCount = Utilities.ConvertBinaryStringToInteger(binary.ToString());
            data = data.Substring(4);
            for (int i = 0; i < unchangedCount; i++)
            {
                binary = new StringBuilder("");
                binary.Append(Utilities.ConvertIntegerToBinaryString(data[4 * i], 8));
                binary.Append(Utilities.ConvertIntegerToBinaryString(data[4 * i + 1], 8));
                binary.Append(Utilities.ConvertIntegerToBinaryString(data[4 * i + 2], 8));
                binary.Append(Utilities.ConvertIntegerToBinaryString(data[4 * i + 3], 8));
                unchangedBytes.Add(Utilities.ConvertBinaryStringToInteger(binary.ToString()));
            }
            data = data.Substring(4 * unchangedCount);

            Color[,] input = Utilities.ConvertStringToColorMatrix(data, width, height);
            Bitmap output = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData outputData = output.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, output.PixelFormat);
            // root pixel
            SetPixel(outputData, 0, 0, input[0, 0]);

            // [c3, c2]
            // [c1, c ]

            // P1 predictor: c1
            for (int i = 1; i < width; i++)
            {
                Color c1 = GetPixel(outputData, i - 1, 0);
                Color c = input[i, 0];
                int r = unchangedBytes.Contains(i * 3) ? c.R : c.R + c1.R - BIAS;
                int g = unchangedBytes.Contains(i * 3 + 1) ? c.G : c.G + c1.G - BIAS;
                int b = unchangedBytes.Contains(i * 3 + 2) ? c.B : c.B + c1.B - BIAS;
                SetPixel(outputData, i, 0, Color.FromArgb(r, g, b));
            }
            // P2 predictor: c2
            for (int j = 1; j < height; j++)
            {
                Color c2 = GetPixel(outputData, 0, j - 1);
                Color c = input[0, j];
                int r = unchangedBytes.Contains(j * width * 3) ? c.R : c.R + c2.R - BIAS;
                int g = unchangedBytes.Contains(j * width * 3 + 1) ? c.G : c.G + c2.G - BIAS;
                int b = unchangedBytes.Contains(j * width * 3 + 2) ? c.B : c.B + c2.B - BIAS;
                SetPixel(outputData, 0, j, Color.FromArgb(r, g, b));
            }
            // P4 predictor: c1 + c2 - c3
            for (int i = 1; i < width; i++)
            {
                for (int j = 1; j < height; j++)
                {

                    Color c1 = GetPixel(outputData, i - 1, j);
                    Color c2 = GetPixel(outputData, i, j - 1);
                    Color c3 = GetPixel(outputData, i - 1, j - 1);
                    Color c = input[i, j];
                    int r = unchangedBytes.Contains(j * width * 3 + i * 3) ? c.R : c.R + (c1.R + c2.R - c3.R) - BIAS;
                    int g = unchangedBytes.Contains(j * width * 3 + i * 3 + 1) ? c.G : c.G + (c1.G + c2.G - c3.G) - BIAS;
                    int b = unchangedBytes.Contains(j * width * 3 + i * 3 + 2) ? c.B : c.B + (c1.B + c2.B - c3.B) - BIAS;
                    SetPixel(outputData, i, j, Color.FromArgb(r, g, b));
                }
            }
            output.UnlockBits(outputData);
            return output;
        }

        public unsafe Color GetPixel(BitmapData bitmapData, int x, int y)
        {
            int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
            byte* firstPixel = (byte*)bitmapData.Scan0;
            byte* line = firstPixel + (y * bitmapData.Stride);
            int i = x * bytesPerPixel;
            int b = line[i];
            int g = line[i + 1];
            int r = line[i + 2];
            return Color.FromArgb(r, g, b);
        }

        public unsafe void SetPixel(BitmapData bitmapData, int x, int y, Color color)
        {
            int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
            byte* firstPixel = (byte*)bitmapData.Scan0;
            byte* line = firstPixel + (y * bitmapData.Stride);
            int i = x * bytesPerPixel;
            line[i] = color.B;
            line[i + 1] = color.G;
            line[i + 2] = color.R;
        }
    }
}
