using System;
using System.Collections.Generic;
using System.Text;

namespace TachographCardStructure.Types
{
    public class Text : IToByte, IGetLength
    {
        public byte CodePage;
        private Encoding _enc;
        private byte[] _content;
        public int MaxLength;
        public int Length { get => _content.Length; }
        public byte[] Content {
            get => _content;
            set
            {
                var size = MaxLength > 0 ? MaxLength : value.Length;
                _content = new byte[size];
                Array.Copy(value, _content, size);
            }
        }

        public Text(int MaxLength)
        {
            this.MaxLength = MaxLength;
        }

        public Text(byte codePage, string text) 
        {
            SetCodePage(codePage); 
            SetText(text);
        }

        public string GetCodePage()
        {
            return _enc is Encoding ? _enc.EncodingName : "not set";
        }

        public void SetCodePage(byte codePage)
        {
            _enc = CodePages.GetEncoding(codePage);
            CodePage = codePage;
        }

        public void SetText(string text)
        {
            var utf8enc = Encoding.UTF8;
            var bytes = utf8enc.GetBytes(text);
            Content = Encoding.Convert(utf8enc, _enc, bytes);
        }



        public override string ToString()
        {
            return _enc.GetString(Content);
        }

        public byte[] ToByte()
        {
            byte[] t_arr = new byte[GetLength()];
            t_arr[0] = CodePage;
            Array.Copy(Content, 0, t_arr, 1, Content.Length);

            return t_arr;
        }

        public ushort GetLength()
        {
            return (ushort)(Content.Length + 1);
        }
    }
}
