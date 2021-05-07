using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    public abstract class NO2bytes : IToByte, IGetLength
    {
        public ushort No { get; set; }

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
    }
}
