using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TachographCardStructure
{
    public class CardChipIdentification : MainFile
    {
        public byte[] IcSerialNumber { get; set; } = new byte[4]; // 4bytes
        public byte[] ICManufacturingReferences { get; set; } = new byte[4]; // 4bytes

        public CardChipIdentification() : base(new byte[] { 0x00, 0x05 }, 0x00)
        {
            IcSerialNumber = new byte[]{ 0x24, 0x18, 0x3C, 0x0D };
            ICManufacturingReferences = new byte[] { 0x52, 0x79, 0x87, 0x15 };

        }
        public override ushort GetLength()
        {
            return (ushort)(IcSerialNumber.Length + ICManufacturingReferences.Length);
        }

        public override byte[] ToByte()
        {
            return new List<byte>()
                .Concat(base.ToByte())
                .Concat(IcSerialNumber)
                .Concat(ICManufacturingReferences)
                .ToArray();
        }
    }
}
