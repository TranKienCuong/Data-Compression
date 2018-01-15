using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public class DifferentialImageCoding
    {
        public Bitmap Encode(Bitmap input)
        {
            Bitmap output = new Bitmap(input.Width, input.Height);
            // root pixel
            output.SetPixel(0, 0, input.GetPixel(0, 0));

            // [c3, c2]
            // [c1, c ]

            // P1 predictor: c1
            for (int i = 1; i < input.Width; i++)
            {
                Color c1 = input.GetPixel(i - 1, 0);
                Color c =  input.GetPixel(i, 0);
                int r = c.R - c1.R;
                int g = c.G - c1.G;
                int b = c.B - c1.B;
                output.SetPixel(i, 0, Color.FromArgb(r, g, b));
            }
            // P2 predictor: c2
            for (int i = 1; i < input.Height; i++)
            {
                Color c2 = input.GetPixel(0, i - 1);
                Color c = input.GetPixel(0, i);
                int r = c.R - c2.R;
                int g = c.G - c2.G;
                int b = c.B - c2.B;
                output.SetPixel(0, i, Color.FromArgb(r, g, b));
            }
            // P4 predictor: c1 + c2 - c3
            for (int i = 1; i < input.Width; i++)
            {
                for (int j = 1; j < input.Height; j++)
                {
                    Color c1 = input.GetPixel(i - 1, j);
                    Color c2 = input.GetPixel(i, j - 1);
                    Color c3 = input.GetPixel(i - 1, j - 1);
                    Color c = input.GetPixel(i, j);
                    int r = c.R - (c1.R + c2.R - c3.R);
                    int g = c.G - (c1.G + c2.G - c3.G);
                    int b = c.B - (c1.B + c2.B - c3.B);
                    output.SetPixel(0, i, Color.FromArgb(r, g, b));
                }
            }
            return output;
        }

        public Bitmap Decode(Bitmap input)
        {
            Bitmap output = new Bitmap(input.Width, input.Height);
         
            return output;
        }
    }
}
