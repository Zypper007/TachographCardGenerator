using System;
using System.Collections.Generic;
using System.Text;
using TachographCardStructure.Types;
using System.Linq;

namespace TachographCardStructure
{
    public class CardIccIdentification : MainFile
    {
        public byte ClockStop { get; set; } = 0x00;// 1byte
        public ExtendedSerialNumber CardExtendedSerialNumber { get; set; } // 8bytes
        public IA5String CardApprovalNumber { get; set; } = new IA5String(8); // 8 bytes
        public byte CardPersonaliserID { get; set; } = 0x89; // 1 byte
        public EmbedderIcAssemblerID EmbedderIcAssemblerID { get; set; } // 5bytes
        public ushort IcIdentifier; // 2bytes

        public CardIccIdentification(byte clockStop, 
            ExtendedSerialNumber cardExtendedSerialNumber, 
            string cardApprovalNumber,
            byte cardPersonalizerID,
            EmbedderIcAssemblerID embedderIcAssembledID,
            ushort icIdentifier) : base(new byte[] {0x00, 0x02})
        {
            ClockStop = clockStop;
            CardExtendedSerialNumber = cardExtendedSerialNumber;
            CardApprovalNumber.SetText(cardApprovalNumber);
            CardPersonaliserID = cardPersonalizerID;
            EmbedderIcAssemblerID = embedderIcAssembledID;
            IcIdentifier = icIdentifier;
            CardApprovalNumber.SetText("e1 221  ");
        }

        public override ushort GetLength()
        {
            return Encrypted == 0x01 ? base.GetLength() : (ushort)(1 + 
                CardExtendedSerialNumber.GetLength()
                + CardApprovalNumber.GetLength()
                + 1 
                + EmbedderIcAssemblerID.GetLength() 
                + 2);
        }

        override public byte[] ToByte()
        {
            var icI_bytes = BitConverter.GetBytes(IcIdentifier);
            if (BitConverter.IsLittleEndian) icI_bytes = icI_bytes.Reverse().ToArray() ;

            return Encrypted == 0x01 ? base.ToByte() : new List<byte>()
                .Concat(base.ToByte())
                .Append(ClockStop)
                .Concat(CardExtendedSerialNumber.ToByte())
                .Concat(CardApprovalNumber.ToByte())
                .Append(CardPersonaliserID)
                .Concat(EmbedderIcAssemblerID.ToByte())
                .Concat(icI_bytes)
                .ToArray();
        }
    }

}
