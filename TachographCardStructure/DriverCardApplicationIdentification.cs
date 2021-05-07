using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class DriverCardApplicationIdentification : MainFile
    {
        public EquipmentType TypeOfTachographCardID { get; set; } = EquipmentType.DriverCard; // 1byte
        public CardStructureVersion CardStructureVersion { get; set; } = CardStructureVersion.Gen1; // 2 byte
        public NoOfEventsPerType NoOfEventsPerType = new NoOfEventsPerType() { No = 0x0C }; // 1byte
        public NoOfFaultsPerType NoOfFaultsPerType = new NoOfFaultsPerType() { No = 0x18 }; // 1byte
        public CardActibityLengthRange ActibityLengthRange { get; set; } = new CardActibityLengthRange(); // 2bytes 13776
        public NoOfCardVehicleRecords NoOfCardVehicleRecords { get; set; } = new NoOfCardVehicleRecords(); // 2bytes 200
        public NoOfCardPlaceRecords NoOfCardPlaceRecords { get; set; } = new NoOfCardPlaceRecords(); // 1 byte 112

        public DriverCardApplicationIdentification() : base(new byte[] {0x05, 0x01}, 0x00)
        {

        }

        public override ushort GetLength()
        {
            return (ushort)(TypeOfTachographCardID.GetLength() 
                            + CardStructureVersion.GetLength()
                            + NoOfEventsPerType.GetLength() 
                            + NoOfFaultsPerType.GetLength()
                            + ActibityLengthRange.GetLength() 
                            + NoOfCardVehicleRecords.GetLength()
                            + NoOfCardPlaceRecords.GetLength());
        }

        public override byte[] ToByte()
        {
            return new List<byte>()
                .Concat(base.ToByte())
                .ToArray();
        }
    }
}
