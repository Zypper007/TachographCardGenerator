using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TachographCardStructure.Types
{
    public class IA5String: IToByte, IGetLength
    {
        private static Encoding ASCII = Encoding.ASCII;
        private byte[] _b;


        public IA5String(int MaxLength, string text="")
        {
            _b = new byte[MaxLength];

            for (var i = 0; i < _b.Length; i++) _b[i] = 0x20;

            if (text != "") SetText(text);
        }


        public void SetText(string text)
        {
            var _bytes = ASCII.GetBytes(text);

            foreach (byte b in _bytes) if (b > 127) throw new Exception($"IA5Strings byte[{b}] over scale");

            var size = _bytes.Length > _b.Length ? _b.Length : _bytes.Length;
            Array.Copy(_bytes, _b, size);
        }

        public override string ToString()
        {
            return ASCII.GetString(_b);
        }

        public byte[] ToByte()
        {
            return _b;
        }

        public ushort GetLength()
        {
            return (ushort)_b.Length;
        }
    }
}
