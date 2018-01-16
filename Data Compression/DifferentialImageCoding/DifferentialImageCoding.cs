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
        public string Encode(Bitmap input)
        {
            StringBuilder result = new StringBuilder("");

            int width = input.Width;
            int height = input.Height;
            Color[,] output = new Color[width, height];
            Color[,] signs = new Color[width, height];

            BitmapData inputData = input.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, input.PixelFormat);

            // root pixel
            output[0, 0] = GetPixel(inputData, 0, 0);
            signs[0, 0] = Color.FromArgb('0', '0', '0');

            // [c3, c2]
            // [c1, c ]

            // P1 predictor: c1
            for (int i = 1; i < width; i++)
            {
                Color c1 = GetPixel(inputData, i - 1, 0);
                Color c =  GetPixel(inputData, i, 0);
                int r = c.R - c1.R; int rsign = (r > 0) ? '0' : '1';
                int g = c.G - c1.G; int gsign = (g > 0) ? '0' : '1';
                int b = c.B - c1.B; int bsign = (b > 0) ? '0' : '1';
                output[i, 0] = Color.FromArgb(Math.Abs(r), Math.Abs(g), Math.Abs(b));
                signs[i, 0] = Color.FromArgb(rsign, gsign, bsign);
            }
            // P2 predictor: c2
            for (int i = 1; i < height; i++)
            {
                Color c2 = GetPixel(inputData, 0, i - 1);
                Color c = GetPixel(inputData, 0, i);
                int r = c.R - c2.R; int rsign = (r > 0) ? '0' : '1';
                int g = c.G - c2.G; int gsign = (g > 0) ? '0' : '1';
                int b = c.B - c2.B; int bsign = (b > 0) ? '0' : '1';
                output[0, i] = Color.FromArgb(Math.Abs(r), Math.Abs(g), Math.Abs(b));
                signs[0, i] = Color.FromArgb(rsign, gsign, bsign);
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
                    int r = c.R - (c1.R/* + c2.R - c3.R*/); int rsign = (r > 0) ? '0' : '1';
                    int g = c.G - (c1.G/* + c2.G - c3.G*/); int gsign = (g > 0) ? '0' : '1';
                    int b = c.B - (c1.B/* + c2.B - c3.B*/); int bsign = (b > 0) ? '0' : '1';
                    output[i, j] = Color.FromArgb(Math.Abs(r), Math.Abs(g), Math.Abs(b));
                    signs[i, j] = Color.FromArgb(rsign, gsign, bsign);
                }
            }
            input.UnlockBits(inputData);

            string binarySignsString = Utilities.ConvertColorMatrixToString(signs, width, height);
            while (binarySignsString.Length % 8 != 0)
                binarySignsString += "0";
            List<int> ints = Utilities.ConvertBinaryStringToListInt(binarySignsString);
            foreach (int i in ints)
                result.Append((char)i);
            result.Append(Utilities.ConvertColorMatrixToString(output, width, height));
            return result.ToString();
        }

        public Bitmap Decode(Bitmap input)
        {
            int width = input.Width;
            int height = input.Height;
            Bitmap output = new Bitmap(width, height);
            
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
    }
}
