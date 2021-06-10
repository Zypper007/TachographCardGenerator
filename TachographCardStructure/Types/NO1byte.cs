using System;
using System.Collections.Generic;
using System.Text;

namespace TachographCardStructure.Types
{
    public class NO1byte: IToByte, IGetLength
    {
        public byte No { get; internal set; }


        public NO1byte(byte no)
        {
            No = no;
        }

        public byte[] ToByte()
        {
            return new byte[1] { No };
        }

        public ushort GetLength()
        {
            return 1;
        }

        public static implicit operator int(NO1byte no) => no.No;
    }
}
