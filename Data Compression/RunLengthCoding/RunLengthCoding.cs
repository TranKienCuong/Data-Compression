using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public class RunLengthCoding
    {
        public byte[] Encode(byte[] data)
        {
            StringBuilder result = new StringBuilder("");
            for (int i = 0; i < data.Length;)
            {
                char c = (char)data[i];
                int count = 1;
                for (int j = i + 1; j < data.Length; j++)
                {
                    if (data[i] != data[j] || count == 255)
                        break;
                    count++;
                }
                result.Append(c);
                result.Append((char)count);
                i += count;
            }
            return Utilities.ConvertStringToBytes(result.ToString());
        }

        public byte[] Decode(byte[] data)
        {
            StringBuilder result = new StringBuilder("");
            for (int i = 0; i < data.Length; i += 2)
            {
                char c = (char)data[i];
                int count = data[i + 1];
                result.Append(c, count);
            }
            return Utilities.ConvertStringToBytes(result.ToString());
        }
    }
}
