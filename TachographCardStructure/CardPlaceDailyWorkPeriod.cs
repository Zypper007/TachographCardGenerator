using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class CardPlaceDailyWorkPeriod : MainFile
    {
        private bool isLooped = false;
        private byte _pointer;
        private byte pointer
        {
            get => _pointer;
            set => _pointer = (byte)(value % PlaceRecords.Length);
        }

        public NO1byte PlacePointerNewestRecord
        {
            get
            {
                if (_pointer == 0 && isLooped == false) return new NO1byte(0);
                else return new NO1byte((byte)(pointer - 1));
            }
        }
        public PlaceRecord[] PlaceRecords { get; private set; }

        public CardPlaceDailyWorkPeriod(NO1byte NoOfCardPlaceRecords): base(new byte[] {0x05, 0x06})
        {
            PlaceRecords = new PlaceRecord[NoOfCardPlaceRecords];
            for (var i = 0; i < PlaceRecords.Length; i++) PlaceRecords[i] = new PlaceRecord();
        }

        public CardPlaceDailyWorkPeriod(byte[] encryptedData) : base(new byte[] { 0x05, 0x06 }, encryptedData) { }

        public CardPlaceDailyWorkPeriod Push(PlaceRecord PlaceRecord)
        {
            PlaceRecords[pointer] = PlaceRecord;
            byte newPointer = (byte)(pointer + 1);
            if (newPointer == PlaceRecords.Length) isLooped = true;
            pointer = newPointer;
            return this;
        }

        public override ushort GetLength()
        {
            if (Encrypted == 0x01)
            {
                return base.GetLength();
            }
            else
            {
                var sum = 1; // 1byte PlacePointerNewestRecord

                foreach (var record in PlaceRecords) sum += record.GetLength();

                return (ushort)sum;
            }
        }

        public override byte[] ToByte()
        {
            if (Encrypted == 0x01)
            {
                return base.ToByte();
            }
            else
            {
                IEnumerable<byte> sum = new List<byte>().Concat(base.ToByte()).Concat(PlacePointerNewestRecord.ToByte());

                foreach (var record in PlaceRecords) sum = sum.Concat(record.ToByte());

                return sum.ToArray();
            }
        }

    }
}
