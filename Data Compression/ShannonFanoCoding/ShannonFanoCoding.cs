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

            Dictionary<Symbol, string> codeWords = new Dictionary<Symbol, string>();
            BuildCodewords(symbols, codeWords, 0, symbols.Count, n, "");

            result.Append((char)(symbols.Count - 1));
            for (int i = 0; i < symbols.Count; i++)
            {
                Symbol symbol = symbols[i];
                string code = codeWords[symbol];
                result.Append((char)symbol.Data);
                result.Append((char)code.Length);
                while (code.Length % 8 != 0)
                    code += "0";
                List<int> ints = Utilities.ConvertBinaryStringToListInt(code);
                foreach (int x in ints)
                    result.Append((char)x);
            }

            StringBuilder binaryString = new StringBuilder("");
            for (int i = 0; i < n; i++)
            {
                string code = codeWords[new Symbol(data[i])];
                binaryString.Append(code);
            }
            int redundantBits = 0;
            while (binaryString.Length % 8 != 0)
            {
                binaryString.Append("0");
                redundantBits++;
            }
            result.Append((char)redundantBits);
            List<int> intes = Utilities.ConvertBinaryStringToListInt(binaryString.ToString());
            foreach (int i in intes)
                result.Append((char)i);

            return Utilities.ConvertStringToBytes(result.ToString());
        }

        public byte[] Decode(byte[] data)
        {
            StringBuilder result = new StringBuilder("");
            int symbolsCount = data[0] + 1;
            int i = 1;
            Dictionary<string, Symbol> codeWords = new Dictionary<string, Symbol>();
            for (int k = 0; k < symbolsCount; k++)
            {
                Symbol symbol = new Symbol(data[i++]);
                int bits = data[i++];
                int bytes = (bits - 1) / 8 + 1;
                List<int> ints = new List<int>();
                for (int j = i; j < i + bytes; j++)
                    ints.Add(data[j]);
                string binary = Utilities.ConvertListIntToBinaryString(ints);
                string code = binary.Substring(0, bits);
                codeWords.Add(code, symbol);
                i += bytes;
            }
            int redundantBits = data[i++];
            List<int> intes = new List<int>();
            for (; i < data.Length; i++)
                intes.Add(data[i]);
            string binaryString = Utilities.ConvertListIntToBinaryString(intes);
            if (redundantBits != 0)
                binaryString = binaryString.Remove(binaryString.Length - redundantBits);
            StringBuilder sb = new StringBuilder("");
            for (int j = 0; j < binaryString.Length; j++)
            {
                sb.Append(binaryString[j]);
                string s = sb.ToString();
                if (codeWords.ContainsKey(s))
                {
                    result.Append((char)codeWords[s].Data);
                    sb = new StringBuilder("");
                }
            }

            return Utilities.ConvertStringToBytes(result.ToString());
        }

        void BuildCodewords(List<Symbol> symbols, Dictionary<Symbol, string> codeWords, int start, int end, int n, string code)
        {
            int n1 = 0;
            int n2 = n;
            int i = start;
            for (; i < end; i++)
            {
                Symbol symbol = symbols[i];
                n1 += symbol.Frequency;
                n2 -= symbol.Frequency;
                if (n1 > n2)
                {
                    int d1 = n1 - n2;
                    n1 -= symbol.Frequency;
                    n2 += symbol.Frequency;
                    int d2 = n2 - n1;
                    if (d1 < d2)
                    {
                        i++;
                        n1 += symbol.Frequency;
                        n2 -= symbol.Frequency;
                    }
                    break;
                }
            }
            for (int j = start; j < end; j++)
                codeWords[symbols[j]] = code;
            if (start >= end - 1)
                return;
            BuildCodewords(symbols, codeWords, start, i, n1, code + "0");
            BuildCodewords(symbols, codeWords, i, end, n2, code + "1");
        }
    }
}
