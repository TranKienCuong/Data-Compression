using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public class HuffmanCoding
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
            List<Node> symbols = new List<Node>();
            NodeComparer nodeComparer = new NodeComparer();
            foreach (var f in frequency)
                symbols.Add(new Node(new Symbol(f.Key, f.Value)));
            symbols.Sort(nodeComparer);

            List<Node> nodes = new List<Node>(symbols);
            Dictionary<Node, string> codeWords = new Dictionary<Node, string>();
            int symbolCode = 256;
            while (nodes.Count != 1)
            {
                Node left = nodes.ElementAt(0);
                Node right = nodes.ElementAt(1);
                int freq = left.Info.Frequency + right.Info.Frequency;
                Node parent = new Node(symbolCode++, freq, left, right);
                nodes.RemoveAt(0);
                nodes.RemoveAt(0);
                nodes.Add(parent);
                nodes.Sort(nodeComparer);
            }
            codeWords[nodes[0]] = "";
            BuildCodewords(nodes[0], codeWords);

            result.Append((char)(symbols.Count - 1));
            for (int i = 0; i < symbols.Count; i++)
            {
                Node node = symbols[i];
                string code = codeWords[node];
                result.Append((char)node.Info.Data);
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
                string code = codeWords[new Node(data[i])];
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

        void BuildCodewords(Node node, Dictionary<Node, string> codeWords)
        {
            Node left = node.Left;
            if (left != null)
            {
                codeWords[left] = codeWords[node] + "0";
                BuildCodewords(left, codeWords);
            }
            Node right = node.Right;
            if (right != null)
            {
                codeWords[right] = codeWords[node] + "1";
                BuildCodewords(right, codeWords);
            }
        }
    }
}
