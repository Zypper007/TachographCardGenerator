using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    abstract public class CardData : MainFile
    {
        protected CardData(byte[] id) : base(id){}

        protected CardData(byte[] id, byte[] encryptedData) : base(id, encryptedData) {}

        protected ushort _getLength(CardRecord[,] cardRecords)
        {
            var length = 0;

            for(var row = 0; row < cardRecords.GetLength(0); row++)
            {
                for(var col = 0; col < cardRecords.GetLength(1); col++)
                {
                    length += cardRecords[row, col].GetLength();
                }
            }
            return (ushort)length;
        }
        protected byte[] _toByte(CardRecord[,] cardRecords)
        {
            IEnumerable<byte> bytes = new List<byte>();

            for (var row = 0; row < cardRecords.GetLength(0); row++)
            {
                for (var col = 0; col < cardRecords.GetLength(1); col++)
                {
                    bytes = bytes.Concat(cardRecords[row, col].ToByte());
                }
            }
            return bytes.ToArray();
        }

        public override ushort GetLength()
        {
            return base.GetLength();
        }

        override public byte[] ToByte()
        {
            return base.ToByte();
        }
    }
}
