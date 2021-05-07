using System;
using System.Collections.Generic;
using System.Text;
using TachographCardStructure.Types;
using System.Linq;

namespace TachographCardStructure
{
    public class CardIccIdentification : MainFile
    {
        public byte clockStop { get; set; } = 0x00;// 1byte
        public ExtendedSerialNumber CardExtendedSerialNumber { get; set; } = new ExtendedSerialNumber(); // 8bytes
        public IA5String CardApprovalNumber { get; set; } = new IA5String(8); // 8 bytes
        public byte CardPersonaliserID { get; set; } = 0x89; // 1 byte
        public EmbedderIcAssemblerID EmbedderIcAssemblerID { get; set; } = new EmbedderIcAssemblerID(); // 5bytes
        public byte[] icIdentifier = new byte[] { 0x15, 0x37 }; // 2bytes

        protected ushort plyloadSize = 25;
        protected byte[] id = new byte[] { 0x00, 0x02 };
        protected bool encrpted = false;

        public CardIccIdentification() : base(new byte[] {0x00, 0x02}, 0x00)
        {
            CardApprovalNumber.SetText("e1 221  ");
        }

        public override ushort GetLength()
        {
            return (ushort)(1 + CardExtendedSerialNumber.GetLength() + CardApprovalNumber.GetLength() + 1 + EmbedderIcAssemblerID.GetLength() + 2);
        }

        override public byte[] ToByte()
        {
            return new List<byte>()
                .Concat(base.ToByte())
                .Append(clockStop)
                .Concat(CardExtendedSerialNumber.ToByte())
                .Concat(CardApprovalNumber.ToByte())
                .Append(CardPersonaliserID)
                .Concat(EmbedderIcAssemblerID.ToByte())
                .Concat(icIdentifier)
                .ToArray();
        }
    }

}
