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
        public string Encode(string data, bool losslessJPEG)
        {
            string result = "";
            if (!losslessJPEG)
            {
                for (int i = 0; i < data.Length; )
                {
                    char c = data[i];
                    int count = 1;
                    for (int j = i + 1; j < data.Length; j++)
                    {
                        if (data[i] != data[j])
                            break;
                        count++;
                    }
                    result += c.ToString() + ((char)count).ToString();
                    i += count;
                }
            }
            else
            {
                
            }
            return result;
        }

        public string Decode(string data)
        {
            string result = "";
            for (int i = 0; i < data.Length; i += 2)
            {
                char c = data[i];
                int count = data[i + 1];
                for (int j = 0; j < count; j++)
                    result += c.ToString();
            }
            return result;
        }
    }
}
