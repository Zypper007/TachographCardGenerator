using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TachographCardStructure
{
    public class CardChipIdentification : MainFile
    {
        public uint IcSerialNumber { get; set; }  // 4bytes
        public uint ICManufacturingReferences { get; set; }  // 4bytes

        public CardChipIdentification(uint serialNumber, uint icManufaturingReferences) : base(new byte[] { 0x00, 0x05 })
        {
            IcSerialNumber = serialNumber;
            ICManufacturingReferences = icManufaturingReferences;

        }
        public override ushort GetLength()
        {
            return Encrypted == 0x01 ? base.GetLength() : (ushort)8;
        }

        public override byte[] ToByte()
        {
            var icS_bytes = BitConverter.GetBytes(IcSerialNumber);
            var icM_bytes = BitConverter.GetBytes(ICManufacturingReferences);

            if(BitConverter.IsLittleEndian)
            {
                icS_bytes = icS_bytes.Reverse().ToArray();
                icM_bytes = icM_bytes.Reverse().ToArray();
            }

            return Encrypted == 0x01 ? base.ToByte() : new List<byte>()
                .Concat(base.ToByte())
                .Concat(icS_bytes)
                .Concat(icM_bytes)
                .ToArray();
        }
    }
}
