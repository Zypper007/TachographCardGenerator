using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    public class DriverCardHolderIdentification : IToByte, IGetLength
    {
        public HolderName CardHolderName { get; private set; }
        public Datef CardHolderBirthDate { get; private set; }
        public Language CardHolderPreferredLanguage { get; private set; }

        public DriverCardHolderIdentification(HolderName CardHolderName, Datef CardHolderBirthDate, Language CardHolderPreferredLanguage)
        {
            this.CardHolderName = CardHolderName;
            this.CardHolderBirthDate = CardHolderBirthDate;
            this.CardHolderPreferredLanguage = CardHolderPreferredLanguage;
        }

        public byte[] ToByte()
        {
            return new List<byte>()
                .Concat(CardHolderName.ToByte())
                .Concat(CardHolderBirthDate.ToByte())
                .Concat(CardHolderPreferredLanguage.ToByte())
                .ToArray();
        }

        public ushort GetLength()
        {
            return (ushort)(CardHolderBirthDate.GetLength()
                    + CardHolderName.GetLength()
                    + CardHolderPreferredLanguage.GetLength());
        }
    }
}
