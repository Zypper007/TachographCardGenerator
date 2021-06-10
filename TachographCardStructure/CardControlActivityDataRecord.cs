using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class CardControlActivityDataRecord : MainFile
    {
        public ControlType ControlType { get; private set; }
        public TimeReal ControlTime { get; private set; }
        public FullCardNumber ControlCardNumber { get; private set; }
        public VehicleRegistrationIdentification ControlVehicleRegistration { get; private set; }
        public TimeReal ControlDownloadPeroidBegin { get; private set; }
        public TimeReal ControlDownloadPeroidEnd { get; private set; }

        public CardControlActivityDataRecord() : base(new byte[] { 0x05, 0x08 })
        {
            ControlType = new ControlType();
            ControlTime = new TimeReal(0);
            ControlCardNumber = new FullCardNumber();
            ControlVehicleRegistration = new VehicleRegistrationIdentification();
            ControlDownloadPeroidBegin = new TimeReal(0);
            ControlDownloadPeroidEnd = new TimeReal(0);
        }

        public CardControlActivityDataRecord(
            ControlType ControlType,
            TimeReal ControlTime,
            FullCardNumber ControlCardNumber,
            VehicleRegistrationIdentification ControlVehicleRegistration,
            TimeReal ControlDownloadPeroidBegin,
            TimeReal ControlDownloadPeroidEnd
        ) : base(new byte[] { 0x05, 0x08 })
        {
            this.ControlType = ControlType;
            this.ControlTime = ControlTime;
            this.ControlCardNumber = ControlCardNumber;
            this.ControlVehicleRegistration = ControlVehicleRegistration;
            this.ControlDownloadPeroidBegin = ControlDownloadPeroidBegin;
            this.ControlDownloadPeroidEnd = ControlDownloadPeroidEnd;
        }

        public CardControlActivityDataRecord(byte[] EncryptedData) : base(new byte[] {0x05, 0x08}, EncryptedData) {}

        public override ushort GetLength()
        {
            return Encrypted == 0x01 ? base.GetLength() : (ushort)(
                ControlType.GetLength()
                + ControlTime.GetLength()
                + ControlCardNumber.GetLength()
                + ControlVehicleRegistration.GetLength()
                + ControlDownloadPeroidBegin.GetLength()
                + ControlDownloadPeroidEnd.GetLength()
            );
        }

        override public byte[] ToByte()
        {
            return Encrypted == 0x01 ? base.ToByte() : new List<byte>()
                .Concat(base.ToByte())
                .Concat(ControlType.ToByte())
                .Concat(ControlTime.ToByte())
                .Concat(ControlCardNumber.ToByte())
                .Concat(ControlVehicleRegistration.ToByte())
                .Concat(ControlDownloadPeroidBegin.ToByte())
                .Concat(ControlDownloadPeroidEnd.ToByte())
            .ToArray();
        }
    }
}
