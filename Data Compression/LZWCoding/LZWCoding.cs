﻿using System;
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
                int x = code - 1;
                int bitLength = 1;
                for (; x > 0; x /= 2)
                    bitLength++;
                result += ((char)bitLength).ToString();
                result += ConvertIntsToString(output, bitLength);
            }
            else
            {

            }
            return result;
        }

        public string Decode(string data)
        {
            int bitLength = data[0];
            data = data.Substring(1);
            string result = "";
            List<int> input = ConvertStringToInts(data, bitLength);
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

        string ConvertIntsToString(List<int> ints, int bitLength)
        {
            string result = "";
            string binary = Utilities.ConvertListIntToBinaryString(ints, bitLength);
            while (binary.Length % 8 != 0)
                binary += "0";
            for (int i = 0; i < binary.Length; i += 8)
            {
                int x = Utilities.ConvertBinaryStringToInteger(binary.Substring(i, 8));
                result += ((char)x).ToString();
            }
            return result;
        }

        List<int> ConvertStringToInts(string str, int bitLength)
        {
            string binary = "";
            for (int i = 0; i < str.Length; i++)
            {
                binary += Utilities.ConvertIntegerToBinaryString(str[i], 8);
            }
            List<int> result = Utilities.ConvertBinaryStringToListInt(binary, bitLength);
            return result;
        }
    }
}