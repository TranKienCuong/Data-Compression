using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public class CompressedFileInfo
    {
        public string FileName;
        public string Algorithm;
        public long OriginalLength;
        public long CompressedLength;
        public float Ratio;
        public float Percentage;

        public CompressedFileInfo() { }

        public CompressedFileInfo(string FileName, string Algorithm, long OriginalLength, long CompressedLength)
        {
            this.FileName = FileName;
            this.Algorithm = Algorithm;
            this.OriginalLength = OriginalLength;
            this.CompressedLength = CompressedLength;
            this.Ratio = (float) OriginalLength / CompressedLength;
            this.Percentage = 1 / Ratio * 100;
        }
    }
}
