using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class DrivingLicenceInfo : MainFile
    {
        public Name DrivingLicenceIssuingAuthority { get; private set; }
        public NO1byte DrivingLicenceIssuingNation { get; private set; }
        public IA5String DrivingLicenceIssuingNumber { get; private set; } = new IA5String(16);

        public DrivingLicenceInfo(
            Name DrivingLicenceIssuingAuthority,
            NO1byte DrivingLicenceIssuingNation,
            string DrivingLicenceIssuingNumber
            ) : base( new byte[] {0x05, 0x21})
        {
            this.DrivingLicenceIssuingAuthority = DrivingLicenceIssuingAuthority;
            this.DrivingLicenceIssuingNation = DrivingLicenceIssuingNation;
            this.DrivingLicenceIssuingNumber.SetText(DrivingLicenceIssuingNumber);
        }

        public DrivingLicenceInfo(byte[] encryptedData) : base(new byte[] { 0x05, 0x21 }, encryptedData){}

        public override ushort GetLength()
        {
            return Encrypted == 0x01 ? base.GetLength() : (ushort)(
                DrivingLicenceIssuingAuthority.GetLength() + DrivingLicenceIssuingNation.GetLength() + DrivingLicenceIssuingNumber.GetLength()
            );
        }

        override public byte[] ToByte()
        {
            return Encrypted == 0x01 ? base.ToByte() : new List<byte>()
                .Concat(base.ToByte())
                .Concat(DrivingLicenceIssuingAuthority.ToByte())
                .Concat(DrivingLicenceIssuingNation.ToByte())
                .Concat(DrivingLicenceIssuingNumber.ToByte())
                .ToArray();
        }
    }
}
