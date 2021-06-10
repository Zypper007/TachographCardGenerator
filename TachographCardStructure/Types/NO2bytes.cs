using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    public class NO2bytes : IToByte, IGetLength
    {
        public ushort No { get; internal set; }

        public NO2bytes(ushort no)
        {
            No = no;
        }

        public ushort GetLength()
        {
            return 2;
        }

        public byte[] ToByte()
        {
            var bs = BitConverter.GetBytes(No);
            if (BitConverter.IsLittleEndian) bs = bs.Reverse().ToArray();
            return bs;
        }

        public static implicit operator int(NO2bytes no) => no.No;
    }
}
