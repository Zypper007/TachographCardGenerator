using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class CardVehiclesUsed: MainFile
    {
        private bool isLooped = false;
        private ushort _pointer;
        private ushort pointer
        {
            get => _pointer;
            set => _pointer = (ushort)(value % CardVechicleRecords.Length);
        }
        public NO2bytes VehiclePointerNewstRecord 
        { 
            get 
            {
                if (_pointer == 0 && isLooped == false) return new NO2bytes(0);
                else return new NO2bytes((ushort)(pointer - 1));
            }
        }
        public CardVechicleRecord[] CardVechicleRecords { get; private set; }

        public CardVehiclesUsed(NO2bytes NoOfCardVehicleRecords) : base(new byte[] { 0x05, 0x05 })
        {
            CardVechicleRecords = new CardVechicleRecord[NoOfCardVehicleRecords.No];
            for(var i = 0; i < CardVechicleRecords.Length; i++)
            {
                CardVechicleRecords[i] = new CardVechicleRecord();
            }
        }

        public CardVehiclesUsed(byte[] encryptedData) : base(new byte[] { 0x05, 0x05 }, encryptedData){}

        public CardVehiclesUsed Push(CardVechicleRecord CardVechicleRecord)
        {
            CardVechicleRecords[pointer] = CardVechicleRecord;
            ushort newPointer = (ushort)(pointer + 1);
            if (newPointer == CardVechicleRecords.Length) isLooped = true;
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
                var sum = 2; // 2bytes VehiclePointerNewstRecord

                foreach (var record in CardVechicleRecords) sum += record.GetLength();

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
                IEnumerable<byte> sum = new List<byte>().Concat(base.ToByte()).Concat(VehiclePointerNewstRecord.ToByte());

                foreach (var record in CardVechicleRecords) sum = sum.Concat(record.ToByte());

                return sum.ToArray();
            }
        }
    }
}
