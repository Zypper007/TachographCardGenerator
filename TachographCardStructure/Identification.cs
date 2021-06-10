using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class Identification : MainFile
    {
        public CardIdentification CardIdentification { get; set; }
        public DriverCardHolderIdentification DriverCardHolderIdentification { get; set; }

        public Identification(byte[] encryptedData) : base(new byte[] { 0x05, 0x20 }, encryptedData){}

        public Identification(
            CardIdentification CardIdentification, 
            DriverCardHolderIdentification DriverCardHolderIdentification
        ) : base(new byte[] { 0x05, 0x20 })
        {
            this.CardIdentification = CardIdentification;
            this.DriverCardHolderIdentification = DriverCardHolderIdentification;
        }

        public override ushort GetLength()
        {
            return Encrypted == 0x01 ? base.GetLength() : (ushort)(
                CardIdentification.GetLength() + DriverCardHolderIdentification.GetLength()
            );
        }

        override public byte[] ToByte()
        {
            return Encrypted == 0x01 ? base.ToByte() : new List<byte>()
                .Concat(base.ToByte())
                .Concat(CardIdentification.ToByte())
                .Concat(DriverCardHolderIdentification.ToByte())
                .ToArray();
        }

    }
}
