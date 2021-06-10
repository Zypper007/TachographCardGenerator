using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class SpecificConditionRecords : MainFile
    {
        private SpecificConditionRecord[] Records = new SpecificConditionRecord[56];

        public SpecificConditionRecord this[int i]
        {
            get => Records[i];
            set => Records[i] = value;
        }

        public SpecificConditionRecords() : base(new byte[] {0x05, 0x22})
        {
            for (var i = 0; i < Records.Length; i++) Records[i] = new SpecificConditionRecord();
        }

        public SpecificConditionRecords(byte[] EncryptedData) : base(new byte[] {0x05, 0x22}, EncryptedData) {}

        public override ushort GetLength()
        {
            if(Encrypted == 0x01)
            {
                return base.GetLength();
            }
            else
            {
                var sum = 0;

                foreach (var record in Records) sum += record.GetLength();

                return (ushort)sum;
            }
        }

        public override byte[] ToByte()
        {
            if(Encrypted == 0x01)
            {
                return base.ToByte();
            }
            else
            {
                IEnumerable<byte> bytes = base.ToByte();

                foreach (var record in Records) bytes = bytes.Concat(record.ToByte());

                return bytes.ToArray();
            }
        }

    }
}
