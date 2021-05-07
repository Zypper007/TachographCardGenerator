using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure
{
    abstract public class MainFile: IToByte, IGetLength
    {
        public MainFile(byte[] id, byte encrypted)
        {
            ID[0] = id[0];
            ID[1] = id[1];
            Encrypted = encrypted;
        }
        protected byte[] ID { get; set; } = new byte[2];
        protected byte Encrypted { get; set; }

        abstract public ushort GetLength();

        virtual public byte[] ToByte()
        {
            var length = BitConverter.GetBytes(GetLength());
            if (BitConverter.IsLittleEndian) length = length.Reverse().ToArray();

            return new List<byte>()
                .Concat(ID)
                .Append(Encrypted)
                .Concat(length)
                .ToArray();
        }
    }
}
