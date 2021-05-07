using System;
using System.Collections.Generic;
using System.Text;

namespace TachographCardStructure.Types
{
    public class CardStructureVersion : IToByte, IGetLength
    {
        public static CardStructureVersion Gen1 = new CardStructureVersion(new byte[] { 0x00, 0x00 });
        public static CardStructureVersion Gen2 = new CardStructureVersion(new byte[] { 0x00, 0x01 });

        private byte[] _bytes = new byte[2];

        private CardStructureVersion(byte[] ver)
        {
            _bytes[0] = ver[0];
            _bytes[1] = ver[1];
        }

        public ushort GetLength()
        {
            return (ushort)_bytes.Length;
        }

        public byte[] ToByte()
        {
            return _bytes;
        }
    }
}
