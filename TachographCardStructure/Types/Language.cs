using System;
using System.Collections.Generic;
using System.Text;

namespace TachographCardStructure.Types
{
    public class Language: IToByte, IGetLength
    {
        public IA5String Code { get; private set; } = new IA5String(2);

        public Language(string code)
        {
            Code.SetText(code);
        }

        public ushort GetLength()
        {
            return ((IGetLength)Code).GetLength();
        }

        public byte[] ToByte()
        {
            return ((IToByte)Code).ToByte();
        }
    }
}
