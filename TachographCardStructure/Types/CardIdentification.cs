using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure.Types
{
    public class CardIdentification: IToByte, IGetLength
    {
        public NO1byte NationNumeric { get; set; } // 1 byte
        public CardNumber CardNumber { get; set; } // 16 bytes
        public Name CardIssuingAuthorityName { get; set; } // 36 bytes
        public TimeReal CardIssueDate { get; set; } // 4 bytes
        public TimeReal CardValidityBegin { get; set; } // 4 bytes
        public TimeReal CardExpirityDate { get; set; } // 4 bytes

        public CardIdentification(NO1byte nationNumeric, CardNumber cardNumber, Name cardIssuingAuthorityName, TimeReal cardIssueDate, TimeReal cardValidityBegin, TimeReal cardExpirityDate)
        {
            NationNumeric = nationNumeric;
            CardNumber = cardNumber;
            CardIssuingAuthorityName = cardIssuingAuthorityName;
            CardIssueDate = cardIssueDate;
            CardValidityBegin = cardValidityBegin;
            CardExpirityDate = cardExpirityDate;
        }

        public ushort GetLength()
        {
            return (ushort)(NationNumeric.GetLength()
                + CardIssuingAuthorityName.GetLength()
                + CardIssueDate.GetLength()
                + CardNumber.GetLength()
                + CardValidityBegin.GetLength()
                + CardExpirityDate.GetLength());
        }

        public byte[] ToByte()
        {
            return new List<byte>()
                .Concat(NationNumeric.ToByte())
                .Concat(CardNumber.ToByte())
                .Concat(CardIssuingAuthorityName.ToByte())
                .Concat(CardIssueDate.ToByte())
                .Concat(CardValidityBegin.ToByte())
                .Concat(CardExpirityDate.ToByte())
                .ToArray();
        }
    }
}
