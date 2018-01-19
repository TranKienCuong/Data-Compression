using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression
{
    public class SymbolComparer : IComparer<Symbol>
    {
        public int Compare(Symbol x, Symbol y)
        {
            return -x.Frequency.CompareTo(y.Frequency);
        }
    }
}
