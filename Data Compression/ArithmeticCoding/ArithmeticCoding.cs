using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Data_Compression
{
    public class ArithmeticCoding
    {
        public string Encode(string data, bool losslessJPEG)
        {
            string result = "";
            if (!losslessJPEG)
            {
                Dictionary<char, Tuple<double, double>> ranges = GetRanges(data);
                int powOfTen = 1;
                BigInteger low = 0, high = 1;
                double range = 1;
            }
            else
            {

            }
            return result;
        }

        public string Decode(string data)
        {
            string result = "";

            return result;
        }

        Dictionary<char, Tuple<double, double>> GetRanges(string data)
        {
            Dictionary<char, int> frequency = new Dictionary<char, int>();
            for (int i = 0; i < data.Length; i++)
            {
                if (frequency.ContainsKey(data[i]))
                    frequency[data[i]]++;
                else
                    frequency[data[i]] = 1;
            }
            Dictionary<char, Tuple<double, double>> ranges = new Dictionary<char, Tuple<double, double>>();
            double low = 0;
            foreach (var symbol in frequency)
            {
                double probability = symbol.Value / data.Length;
                double high = low + probability;
                ranges[symbol.Key] = new Tuple<double, double>(low, high);
                low = high;
            }
            return ranges;
        }

        int GetFractionLength(double number)
        {
            string str = number.ToString();
            return str.Substring(str.IndexOf(".") + 1).Length;
        }
    }
}
