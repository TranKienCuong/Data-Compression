﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Compression.ShannonFano
{
    public class ShannonFanoCoding
    {
        public byte[] Encode(byte[] data)
        {
            StringBuilder result = new StringBuilder("");
            
            return Utilities.ConvertStringToBytes(result.ToString());
        }

        public byte[] Decode(byte[] data)
        {
            StringBuilder result = new StringBuilder("");
            
            return Utilities.ConvertStringToBytes(result.ToString());
        }
    }
}