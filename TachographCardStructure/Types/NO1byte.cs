using System;
using System.Collections.Generic;
using System.Text;

namespace TachographCardStructure.Types
{
    public abstract class NO1byte: IToByte, IGetLength
    {
        public byte No { get; set; }

        public byte[] ToByte()
        {
            return new byte[1] { No };
        }

        public ushort GetLength()
        {
            return 1;
        }
    }
}
