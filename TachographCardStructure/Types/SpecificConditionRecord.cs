using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    public class SpecificConditionRecord : IToByte, IGetLength
    {
        public TimeReal EntryTime { get; internal set; }
        public byte SpecificConditionType { get; internal set; }

        public SpecificConditionRecord(TimeReal entryTime, byte specificConditionType)
        {
            EntryTime = entryTime;
            SpecificConditionType = specificConditionType;
        }

        public SpecificConditionRecord()
        {
            EntryTime = new TimeReal(0);
            SpecificConditionType = 0x00;
        }

        public byte[] ToByte()
        {
            return EntryTime.ToByte().Append(SpecificConditionType).ToArray();
        }

        public ushort GetLength()
        {
            return (ushort)(1 + EntryTime.GetLength());
        }
    }
}
