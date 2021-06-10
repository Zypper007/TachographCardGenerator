using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class DriverCardApplicationIdentification : MainFile
    {
        public EquipmentType TypeOfTachographCardID { get; set; } // 1byte
        public CardStructureVersion CardStructureVersion { get; set; } // 2 byte
        public NO1byte NoOfEventsPerType { get; set; } // 1byte
        public NO1byte NoOfFaultsPerType { get; set; } // 1byte
        public NO2bytes ActibityLengthRange { get; set; }  // 2bytes 
        public NO2bytes NoOfCardVehicleRecords { get; set; } // 2bytes
        public NO1byte NoOfCardPlaceRecords { get; set; }  // 1 byte 

        public DriverCardApplicationIdentification(
                EquipmentType typeofTachografCardID,
                CardStructureVersion cardStructureVersuin,
                NO1byte noOfEventsPerType,
                NO1byte noOfFaultsPerType,
                NO2bytes actibityLengthRange,
                NO2bytes noOfCardVehicleRecords,
                NO1byte noOfCardPlaceRecords) 
            : base(new byte[] {0x05, 0x01})
        {
            TypeOfTachographCardID = typeofTachografCardID;
            CardStructureVersion = cardStructureVersuin;
            NoOfEventsPerType = noOfEventsPerType;
            NoOfFaultsPerType = noOfFaultsPerType;
            ActibityLengthRange = actibityLengthRange;
            NoOfCardVehicleRecords = noOfCardVehicleRecords;
            NoOfCardPlaceRecords = noOfCardPlaceRecords;
        }

        public DriverCardApplicationIdentification(byte[] encryptedData) : base(new byte[] { 0x05, 0x01 }, encryptedData){}

        public override ushort GetLength()
        {
            return Encrypted == 0x01 ? base.GetLength() :
                    (ushort)(TypeOfTachographCardID.GetLength() 
                            + CardStructureVersion.GetLength()
                            + NoOfEventsPerType.GetLength() 
                            + NoOfFaultsPerType.GetLength()
                            + ActibityLengthRange.GetLength() 
                            + NoOfCardVehicleRecords.GetLength()
                            + NoOfCardPlaceRecords.GetLength());
        }

        public override byte[] ToByte()
        {
            return Encrypted == 0x01 ? base.ToByte() : new List<byte>()
                .Concat(base.ToByte())
                .Concat(TypeOfTachographCardID.ToByte())
                .Concat(CardStructureVersion.ToByte())
                .Concat(NoOfEventsPerType.ToByte())
                .Concat(NoOfFaultsPerType.ToByte())
                .Concat(ActibityLengthRange.ToByte())
                .Concat(NoOfCardVehicleRecords.ToByte())
                .Concat(NoOfCardPlaceRecords.ToByte())
                .ToArray();
        }
    }
}
