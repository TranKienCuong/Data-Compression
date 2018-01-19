using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public class Symbol
    {
        public byte Data;
        public int Frequency;

        public Symbol() { }

        public Symbol(byte data)
        {
            this.Data = data;
        }

        public Symbol(byte data, int frequency)
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
