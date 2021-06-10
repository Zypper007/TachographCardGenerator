using System.Collections.Generic;
using System.Linq;

namespace TachographCardStructure.Types
{
    public class VehicleRegistrationIdentification : IToByte, IGetLength
    {
        public NO1byte VehicleRegistrationNation { get; private set; }
        public VehicleRegistrationNumber VehicleRegistrationNumber { get; private set; }

        public VehicleRegistrationIdentification(NO1byte VehicleRegistrationNation, VehicleRegistrationNumber VehicleRegistrationNumber)
        {
            this.VehicleRegistrationNation = VehicleRegistrationNation;
            this.VehicleRegistrationNumber = VehicleRegistrationNumber;
        }

        public VehicleRegistrationIdentification()
        {
            VehicleRegistrationNation = new NO1byte(0);
            VehicleRegistrationNumber = new VehicleRegistrationNumber(0, "");
        }

        public override string ToString()
        {
            return VehicleRegistrationNumber.ToString();
        }

        public byte[] ToByte()
        {
            return new List<byte>()
                .Concat(VehicleRegistrationNation.ToByte())
                .Concat(VehicleRegistrationNumber.ToByte())
                .ToArray();
        }

        public ushort GetLength()
        {
            return (ushort)(VehicleRegistrationNation.GetLength() + VehicleRegistrationNumber.GetLength());
        }
    }
}