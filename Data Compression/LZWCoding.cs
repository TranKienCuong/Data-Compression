using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public class LZWCoding
    {
        public string Encode(string data, bool losslessJPEG)
        {
            string result = "";
            List<int> output = new List<int>();
            if (!losslessJPEG)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                int code;
                for (code = 0; code < 256; code++)
                    dictionary.Add(((char)code).ToString(), code);
                string s = data[0].ToString();
                for (int i = 1; i < data.Length; i++)
                {
                    char c = data[i];
                    if (dictionary.ContainsKey(s + c.ToString()))
                    {
                        s = s + c.ToString();
                    }
                    else
                    {
                        output.Add(dictionary[s]);
                        dictionary.Add(s + c.ToString(), code++);
                        s = c.ToString();
                    }
                }
                output.Add(dictionary[s]);
                result = FormatIntsToString(output);
            }
            else
            {

            }
            return result;
        }

        public string Decode(string data)
        {
            string result = "";
            List<int> input = FormatStringToInts(data);
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            int code;
            for (code = 0; code < 256; code++)
                dictionary.Add(code, ((char)code).ToString());
            string s = "";
            for (int i = 0; i < input.Count; i++)
            {
                int k = input[i];
                string entry = "";
                if (dictionary.ContainsKey(k))
                    entry = dictionary[k];
                if (entry == "" && s != "")
                    entry = s + s[0].ToString();
                result += entry;
                if (s != "")
                {
                    dictionary[code++] = s + entry[0].ToString(); 
                }
                s = entry;
            }
            return result;
        }

        string FormatIntsToString(List<int> ints)
        {
            string result = "";
            string binary = "";
            foreach (int i in ints)
            {
                binary += ConvertIntegerToBinaryString(i, 9);
            }
            while (binary.Length % 8 != 0)
                binary += "0";
            for (int i = 0; i < binary.Length; i += 8)
            {
                int x = ConvertBinaryStringToInteger(binary.Substring(i, 8));
                result += ((char)x).ToString();
            }
            return result;
        }

        List<int> FormatStringToInts(string str)
        {
            List<int> result = new List<int>();
            string binary = "";
            for (int i = 0; i < str.Length; i++)
            {
                binary += ConvertIntegerToBinaryString(str[i], 8);
            }
            for (int i = 0; i < binary.Length - 9; i += 9)
            {
                int x = ConvertBinaryStringToInteger(binary.Substring(i, 9));
                result.Add(x);
            }
            return result;
        }

        int ConvertBinaryStringToInteger(string input)
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

        string ConvertIntegerToBinaryString(int input, int length)
        {
            string binaryString = "";
            for (int i = 0; i < length; i++)
            {
                binaryString = (input % 2).ToString() + binaryString;
                input /= 2;
            }
            return binaryString;
        }
    }
}
