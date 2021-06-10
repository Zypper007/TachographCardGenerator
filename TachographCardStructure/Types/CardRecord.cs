using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    public class CardRecord: IToByte, IGetLength
    {
        public NO1byte EventFaultType { get; private set; }
        public TimeReal EventBeginTime { get; private set; }
        public TimeReal EventEndTime { get; private set; }
        public VehicleRegistrationIdentification EventVechicleRegistration { get; private set; }

        public CardRecord(NO1byte EventFaultType, TimeReal EventBeginTime, TimeReal EventEndTime, VehicleRegistrationIdentification EventVechicleRegistration)
        {
            this.EventFaultType = EventFaultType;
            this.EventBeginTime = EventBeginTime;
            this.EventEndTime = EventEndTime;
            this.EventVechicleRegistration = EventVechicleRegistration;
        }
        public CardRecord()
        {
            EventFaultType = new NO1byte(0);
            EventBeginTime = new TimeReal(0);
            EventEndTime = new TimeReal(0);
            EventVechicleRegistration = new VehicleRegistrationIdentification(
                new NO1byte(0),
                new VehicleRegistrationNumber(0x00, "")
            );
        }

        public byte[] ToByte()
        {
            return new List<byte>()
                .Concat(EventFaultType.ToByte())
                .Concat(EventBeginTime.ToByte())
                .Concat(EventEndTime.ToByte())
                .Concat(EventVechicleRegistration.ToByte())
                .ToArray();
        }

        public ushort GetLength()
        {
            return (ushort)(EventFaultType.GetLength()
                            + EventBeginTime.GetLength()
                            + EventEndTime.GetLength()
                            + EventVechicleRegistration.GetLength());
        }
    }
}
