using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    public class OdometerShort : IToByte, IGetLength
    {
        private byte[] bytes = new byte[3];
        public UInt32 No
        {
            get
            {
                var b = new byte[] { 0x00 }.Concat(bytes);
                if (BitConverter.IsLittleEndian) b = b.Reverse();

                return BitConverter.ToUInt32(b.ToArray(),0);
            }
        }


        public OdometerShort(UInt32 Value)
        {
            var b = BitConverter.GetBytes(Value);
            if (BitConverter.IsLittleEndian) b = b.Reverse().ToArray();
            Array.Copy(b, 1, bytes, 0, 3);
        }

        public ushort GetLength()
        {
            return (ushort)bytes.Length;
        }

        public byte[] ToByte()
        {
            return bytes;
        }
    }
}
