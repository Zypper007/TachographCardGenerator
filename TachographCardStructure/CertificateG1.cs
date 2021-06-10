using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TachographCardStructure
{
    public class CertificateG1 : MainFile
    {
        public byte[] Sign { get; } = new byte[128] ;

        public byte[] PublicKeyRest { get; } = new byte[58] ;

        public byte[] Issuer { get; } = new byte[8] ;

        public CertificateG1(byte[] id, byte[] sign, byte[] publicRestKey, byte[] issuer ) : base (id)
        {
            Array.Copy(sign, Sign, 128);
            Array.Copy(publicRestKey, PublicKeyRest, 58);
            Array.Copy(issuer, Issuer, 8);
        }

        public override ushort GetLength()
        {
            return (ushort)(Sign.Length + PublicKeyRest.Length + Issuer.Length);
        }

        public override byte[] ToByte()
        {
            return new List<byte>()
                .Concat(base.ToByte())
                .Concat(Sign)
                .Concat(PublicKeyRest)
                .Concat(Issuer)
                .ToArray();
        }
    }
}
