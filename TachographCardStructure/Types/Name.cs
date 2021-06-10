using System;
using System.Collections.Generic;
using System.Text;

namespace TachographCardStructure.Types
{
    public class Name : IToByte, IGetLength
    {
        public byte CodePage { get; private set; } 
        public Encoding Encoding { get; private set; }
        public byte[] Content { get; private set; } = new byte[35];


        public Name(byte codePage, string content)
        {
            CodePage = codePage;
            Encoding = CodePages.GetEncoding(codePage);
            for (var i = 0; i < Content.Length; i++) Content[i] = 0x20;
            SetText(content);
        }


        public void SetText(string text)
        {
            var utf8enc = Encoding.UTF8;
            var bytes = utf8enc.GetBytes(text);
            var en_bytes = Encoding.Convert(utf8enc, Encoding, bytes);
            var size = en_bytes.Length > Content.Length ? Content.Length : en_bytes.Length;
            Array.Copy(en_bytes, Content, size);
        }



        public override string ToString()
        {
            return Encoding.GetString(Content);
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
