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
        private byte[] _bytes {
            get => _b; 
            set
            {
                foreach (byte b in value)
                    if (b > 127) throw new Exception($"IA5Strings byte[{b}] over scale");

                var size = MaxLength > 0 ? MaxLength : value.Length;
                _b = new byte[size];
                Array.Copy(value, _b, size);
            }
        } 

        public int MaxLength;

        public IA5String(int MaxLength)
        {
            this.MaxLength = MaxLength;
        }

        public IA5String(string text)
        {
            SetText(text);
        }

        public void SetText(string text)
        {
            _bytes = ASCII.GetBytes(text);
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
            return (ushort)_bytes.Length;
        }
    }
}
