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
        public string Encode(string data)
        {
            StringBuilder result = new StringBuilder("");
            for (int i = 0; i < data.Length;)
            {
                char c = data[i];
                int count = 1;
                for (int j = i + 1; j < data.Length; j++)
                {
                    if (data[i] != data[j])
                        break;
                    count++;
                }
                result.Append(c);
                result.Append((char)count);
                i += count;
            }
            return result.ToString();
        }

        public string Decode(string data)
        {
            StringBuilder result = new StringBuilder("");
            for (int i = 0; i < data.Length; i += 2)
            {
                char c = data[i];
                int count = data[i + 1];
                for (int j = 0; j < count; j++)
                    result.Append(c);
            }
            return result.ToString();
        }
    }
}
