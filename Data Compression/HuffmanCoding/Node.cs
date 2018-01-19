using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public class Node
    {
        public Node Left { get; private set; }
        public Node Right { get; private set; }
        public Symbol Info { get; private set; }

        public Node() { }

        public Node(int data)
        {
            Info = new Symbol(data);
            Left = null;
            Right = null;
        }

        public Node(Symbol symbol)
        {
            Info = symbol;
            Left = null;
            Right = null;
        }

        public Node(int data, int frequency, Node left, Node right)
        {
            Info = new Symbol();
            Info.Data = data;
            Info.Frequency = frequency;
            Left = left;
            Right = right;
        }

        public override bool Equals(object obj)
        {
            Node o = obj as Node;
            return (Info.Data == o.Info.Data);
        }

        public override int GetHashCode()
        {
            return Info.Data.GetHashCode();
        }
    }
}
