using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class CardFaultsData : CardData
    {
        public CardRecord[,] CardFaultRecords { get; set; }

        public CardFaultsData(CardRecord[,] cardEventRecords) : base(new byte[] { 0x05, 0x03 })
        {
            var row = cardEventRecords.GetLength(0);
            if (row != 2) throw new Exception("[CardEventData::Constructor] CardRecords is too large or too small");

            var col = cardEventRecords.GetLength(1);
            if (col < 12 && col > 24) throw new Exception("[CardEventData::Constructor] CardRecords is too large or too small");

            CardFaultRecords = (CardRecord[,])cardEventRecords.Clone();
        }

        public CardFaultsData(byte[] encryptedData) : base(new byte[] { 0x05, 0x03 }, encryptedData){}

        public CardFaultsData(int NoOfFaultsPerType) : base(new byte[] { 0x05, 0x03 })
        {
            if (NoOfFaultsPerType < 12 && NoOfFaultsPerType > 24) throw new Exception("[CardEventData::Constructor] CardRecords is too large or too small");

            CardFaultRecords = new CardRecord[2, NoOfFaultsPerType];
            for (var row = 0; row < 2; row++)
                for (var col = 0; col < NoOfFaultsPerType; col++)
                    CardFaultRecords[row, col] = new CardRecord();
        }

        public override ushort GetLength()
        {
            return Encrypted == 0x01 ? base.GetLength() : _getLength(CardFaultRecords);
        }

        override public byte[] ToByte()
        {
            return Encrypted == 0x01 ? base.ToByte() : new List<byte>()
                .Concat(base.ToByte())
                .Concat(_toByte(CardFaultRecords))
                .ToArray();
        }
    }
}
