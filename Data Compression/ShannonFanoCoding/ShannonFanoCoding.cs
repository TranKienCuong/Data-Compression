using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public class ShannonFanoCoding
    {
        public byte[] Encode(byte[] data)
        {
            StringBuilder result = new StringBuilder("");
            int n = data.Length;
            Dictionary<byte, int> frequency = new Dictionary<byte, int>();
            for (int i = 0; i < data.Length; i++)
            {
                if (frequency.ContainsKey(data[i]))
                    frequency[data[i]]++;
                else
                    frequency[data[i]] = 1;
            }
            List<Symbol> symbols = new List<Symbol>();
            foreach (var f in frequency)
                symbols.Add(new Symbol(f.Key, f.Value));
            symbols.Sort(new SymbolComparer());


            return Utilities.ConvertStringToBytes(result.ToString());
        }

        public byte[] Decode(byte[] data)
        {
            StringBuilder result = new StringBuilder("");
            
            return Utilities.ConvertStringToBytes(result.ToString());
        }

        void BuildCodingTree(List<Symbol> symbols, int n)
        {
            int n1 = 0;
            int n2 = n;
            foreach (var symbol in symbols)
            {
                n1 += symbol.Frequency;
                n2 -= symbol.Frequency;
                
            }
        }
    }
}
