using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    public class HolderName : IToByte, IGetLength
    {
        public Name HolderSurname { get; private set; } // 36 bytes
        public Name HolderFirstNames { get; private set; } // 36 bytes

        public HolderName(Name holderSurname, Name holderFirstNames)
        {
            HolderSurname = holderSurname;
            HolderFirstNames = holderFirstNames;
        }

        public ushort GetLength()
        {
            return (ushort)(HolderFirstNames.GetLength() + HolderSurname.GetLength());
        }

        public byte[] ToByte()
        {
            return new List<byte>()
                .Concat(HolderSurname.ToByte())
                .Concat(HolderFirstNames.ToByte())
                .ToArray();
        }
    }
}
