using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public class Symbol
    {
        public int Data;
        public int Frequency;

        public Symbol() { }

        public Symbol(int data)
        {
            this.Data = data;
        }

        public Symbol(int data, int frequency)
        {
            this.Data = data;
            this.Frequency = frequency;
        }

        public override bool Equals(object obj)
        {
            Symbol o = obj as Symbol;
            return Data == o.Data;
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }
    }
}
