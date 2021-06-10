using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    public class FullCardNumber : IToByte, IGetLength
    {
        public EquipmentType CardType { get; internal set; }
        public NO1byte CardIssuinMemberState { get; internal set; }
        public CardNumber CardNumber { get; internal set; }

        public FullCardNumber()
        {
            CardType = EquipmentType.Reserved;
            CardIssuinMemberState = new NO1byte(0);
            CardNumber = new CardNumber();
        }

        public FullCardNumber(EquipmentType CardType, NO1byte CardIssuinMemberState, CardNumber CardNumber)
        {
            this.CardType = CardType;
            this.CardIssuinMemberState = CardIssuinMemberState;
            this.CardNumber = CardNumber;
        }

        public ushort GetLength()
        {
            return (ushort)(CardType.GetLength() + CardIssuinMemberState.GetLength() + CardNumber.GetLength());
        }

        public byte[] ToByte()
        {
            return new List<byte>()
                .Concat(CardType.ToByte())
                .Concat(CardIssuinMemberState.ToByte())
                .Concat(CardNumber.ToByte())
            .ToArray();
        }
    }
}
