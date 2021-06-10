using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    // 16 bytes
    public class CardNumber : IToByte, IGetLength
    {
        public IA5String DriverIdentification { get; private set; }
        public IA5String CardConsecutiveIndex { get; private set; }
        public IA5String CardReplacementIndex { get; private set; }
        public IA5String CardRenewalIndex { get; private set; }

        public CardNumber()
        {
            DriverIdentification = new IA5String(13);
            CardConsecutiveIndex = new IA5String(1);
            CardReplacementIndex = new IA5String(1);
            CardRenewalIndex = new IA5String(1);
        }

        public CardNumber(string driverIdentification, string cardConsecutiveIndex, string cardReplacementIndex, string cardRenewalIndex)
        {
            DriverIdentification = new IA5String(13);
            CardConsecutiveIndex = new IA5String(1);
            CardReplacementIndex = new IA5String(1);
            CardRenewalIndex = new IA5String(1);

            DriverIdentification.SetText(driverIdentification);
            CardConsecutiveIndex.SetText(cardConsecutiveIndex);
            CardReplacementIndex.SetText(cardReplacementIndex);
            CardRenewalIndex.SetText(cardRenewalIndex);
        }

        public CardNumber(string driverIdentification, string cardReplacementIndex, string cardRenewalIndex)
        {
            DriverIdentification = new IA5String(14);
            CardConsecutiveIndex = new IA5String(0);
            CardReplacementIndex = new IA5String(1);
            CardRenewalIndex = new IA5String(1);

            DriverIdentification.SetText(driverIdentification);
            CardReplacementIndex.SetText(cardReplacementIndex);
            CardRenewalIndex.SetText(cardRenewalIndex);
        }

        public ushort GetLength()
        {
            return (ushort)(DriverIdentification.GetLength() 
                        + CardConsecutiveIndex.GetLength() 
                        + CardReplacementIndex.GetLength() 
                        + CardRenewalIndex.GetLength());
        }

        public byte[] ToByte()
        {
            return new List<byte>()
                .Concat(DriverIdentification.ToByte())
                .Concat(CardConsecutiveIndex.ToByte())
                .Concat(CardReplacementIndex.ToByte())
                .Concat(CardRenewalIndex.ToByte())
                .ToArray();
        }
    }
}
