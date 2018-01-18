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
        public int Code;

        public Symbol() { }

        public Symbol(byte data, int frequency)
        {
            this.Data = data;
            this.Frequency = frequency;
        }
    }
}
