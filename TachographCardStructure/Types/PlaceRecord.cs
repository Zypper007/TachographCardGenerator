using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    public class PlaceRecord : IToByte, IGetLength
    {
        public TimeReal EntryTime { get; internal set; }
        public NO1byte EntryTypeDailyWorkPeriod { get; internal set; }
        public NO1byte NationalNumeric { get; internal set; }
        public NO1byte RegionNumeric { get; internal set; }
        public OdometerShort VehicleOdometerValue { get; internal set; }

        public PlaceRecord()
        {
            EntryTime = new TimeReal(0);
            EntryTypeDailyWorkPeriod = new NO1byte(0);
            NationalNumeric = new NO1byte(0);
            RegionNumeric = new NO1byte(0);
            VehicleOdometerValue = new OdometerShort(0);
        }

        public PlaceRecord(TimeReal EntryTime, NO1byte EntryTypeDailyWorkPeriod, NO1byte NationalNumeric, NO1byte RegionNumeric, OdometerShort VehicleOdometerValue)
        {
            this.EntryTime = EntryTime;
            this.EntryTypeDailyWorkPeriod = EntryTypeDailyWorkPeriod;
            this.NationalNumeric = NationalNumeric;
            this.RegionNumeric = RegionNumeric;
            this.VehicleOdometerValue = VehicleOdometerValue;
        }

        public ushort GetLength()
        {
            return (ushort)(EntryTime.GetLength()
                 + EntryTypeDailyWorkPeriod.GetLength()
                 + NationalNumeric.GetLength()
                 + RegionNumeric.GetLength()
                 + VehicleOdometerValue.GetLength());
        }

        public byte[] ToByte()
        {
            return new List<byte>()
                .Concat(EntryTime.ToByte())
                .Concat(EntryTypeDailyWorkPeriod.ToByte())
                .Concat(NationalNumeric.ToByte())
                .Concat(RegionNumeric.ToByte())
                .Concat(VehicleOdometerValue.ToByte())
            .ToArray(); 
        }
    }
}
