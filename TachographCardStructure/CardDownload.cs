using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TachographCardStructure.Types;

namespace TachographCardStructure
{
    public class CardDownload : MainFile
    {
        public TimeReal LastCardDownload { get; set; }

        public CardDownload(DateTime date) : base(new byte[] {0x05, 0x0E})
        {
            LastCardDownload = new TimeReal(date);
        }
        public CardDownload(uint ticks) : base(new byte[] { 0x05, 0x0E })
        {
            LastCardDownload = new TimeReal(ticks);
        }
        public CardDownload(byte[] encryptedData): base(new byte[] {0x05, 0x0E}, encryptedData){}

        public override ushort GetLength()
        {
            return Encrypted == 0x01 ? base.GetLength() : ((IGetLength)LastCardDownload).GetLength() ;
        }

        override public byte[] ToByte()
        {
            return Encrypted == 0x01 ? base.ToByte() : base.ToByte().Concat(LastCardDownload.ToByte()).ToArray(); 
        }


    }
}
