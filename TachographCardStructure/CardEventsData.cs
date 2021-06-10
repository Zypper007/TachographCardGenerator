using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class CardEventsData : CardData
    {
        public CardRecord[,] CardEventRecords { get; private set; }

        public CardEventsData(CardRecord[,] cardEventRecords) : base(new byte[] { 0x05, 0x02 })
        {
            var row = cardEventRecords.GetLength(0);
            if (row != 6) throw new Exception("[CardEventData::Constructor] CardRecords is too large or too small");

            var col = cardEventRecords.GetLength(1);
            if (col < 6 && col > 12) throw new Exception("[CardEventData::Constructor] CardRecords is too large or too small");

            CardEventRecords = (CardRecord[,])cardEventRecords.Clone();
        }

        public CardEventsData(byte[] encryptedData) : base(new byte[] { 0x05, 0x02 }, encryptedData){}

        public CardEventsData(int NoOfEventsPerType) : base(new byte[] { 0x05, 0x02 })
        {
            if (NoOfEventsPerType < 6 && NoOfEventsPerType > 12) throw new Exception("[CardEventData::Constructor] CardRecords is too large or too small");

            CardEventRecords = new CardRecord[6, NoOfEventsPerType];
            for (var row = 0; row < 6; row++)
                for (var col = 0; col < NoOfEventsPerType; col++)
                    CardEventRecords[row, col] = new CardRecord();
        }

        public override ushort GetLength()
        {
            return Encrypted == 0x01 ? base.GetLength() : _getLength(CardEventRecords);
        }

        override public byte[] ToByte()
        {
            return Encrypted == 0x01 ? base.ToByte() : new List<byte>()
                .Concat(base.ToByte())
                .Concat(_toByte(CardEventRecords))
                .ToArray();
        }
    }
}
