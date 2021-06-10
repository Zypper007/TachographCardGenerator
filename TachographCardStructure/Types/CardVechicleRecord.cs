using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    public class CardVechicleRecord : IToByte, IGetLength
    {
        public OdometerShort VehicleOdometerBegin { get; private set; }
        public OdometerShort VehicleOdometerEnd { get; private set; }
        public TimeReal VehicleFirstUse { get; private set; }
        public TimeReal VehicleLastUse { get; private set; }
        public VehicleRegistrationIdentification VehicleRegistration { get; private set; }
        public BCDString VuDataBlockCounter { get; private set; } = new BCDString(2);

        public CardVechicleRecord(
            OdometerShort VehicleOdometerBegin,
            OdometerShort VehicleOdometerEnd,
            TimeReal VehicleFirstUse,
            TimeReal VehicleLastUse,
            VehicleRegistrationIdentification VehicleRegistration,
            int VuDataBlockCounter
        )
        {
            this.VehicleOdometerBegin = VehicleOdometerBegin;
            this.VehicleOdometerEnd = VehicleOdometerEnd;
            this.VehicleFirstUse = VehicleFirstUse;
            this.VehicleLastUse = VehicleLastUse;
            this.VehicleRegistration = VehicleRegistration;
            this.VuDataBlockCounter.SetNumber( VuDataBlockCounter );
        }

        public CardVechicleRecord()
        {
            VehicleOdometerBegin = new OdometerShort(0);
            VehicleOdometerEnd = new OdometerShort(0);
            VehicleFirstUse = new TimeReal(0);
            VehicleLastUse = new TimeReal(0);
            VehicleRegistration = new VehicleRegistrationIdentification();
            VuDataBlockCounter.SetNumber(0);
        }

        public ushort GetLength()
        {
            return (ushort)(VehicleOdometerBegin.GetLength()
                + VehicleOdometerEnd.GetLength()
                + VehicleFirstUse.GetLength()
                + VehicleLastUse.GetLength()
                + VehicleRegistration.GetLength()
                + VuDataBlockCounter.GetLength());
        }

        public byte[] ToByte()
        {
            return new List<byte>()
                .Concat(VehicleOdometerBegin.ToByte())
                .Concat(VehicleOdometerEnd.ToByte())
                .Concat(VehicleFirstUse.ToByte())
                .Concat(VehicleLastUse.ToByte())
                .Concat(VehicleRegistration.ToByte())
                .Concat(VuDataBlockCounter.ToByte())
            .ToArray();
        }
    }
}
