using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class CardDriverActivityData : MainFile
    {
        public NO2bytes ActivityPointerOldestDayRecord { get; private set; }
        public NO2bytes ActivityPointerNewestRecord { get; private set; }
        public byte[] data { get; private set; }


        public CardDriverActivityData(NO2bytes activityDailyRecords) : base(new byte[] { 0x05, 0x04 })
        {
            data = new byte[activityDailyRecords];
            ActivityPointerOldestDayRecord = new NO2bytes(0);
            ActivityPointerNewestRecord = new NO2bytes(0);
        }

        internal CardDriverActivityData(
            NO2bytes activityPointerOldestDayRecord,
            NO2bytes activityPointerNewestRecord, 
            ushort activityDailyRecords) : base(new byte[] { 0x05, 0x04 })
        {
            data = new byte[activityDailyRecords];
            ActivityPointerOldestDayRecord = activityPointerOldestDayRecord;
            ActivityPointerNewestRecord = activityPointerNewestRecord;
        }

        public CardDriverActivityData(byte[] encryptedData) : base(new byte[] { 0x05, 0x04 }, encryptedData) { }

        public override ushort GetLength()
        {
            return Encrypted == 0x01 ? base.GetLength() : (ushort) (
                ActivityPointerOldestDayRecord.GetLength()
                + ActivityPointerNewestRecord.GetLength()
                + data.Length
            );
        }

        override public byte[] ToByte()
        { 
            return Encrypted == 0x01 ? base.ToByte() : new List<byte>()
                .Concat(base.ToByte())
                .Concat(ActivityPointerOldestDayRecord.ToByte())
                .Concat(ActivityPointerNewestRecord.ToByte())
                .Concat(data)
                .ToArray();
        }

    }
}
