using System;
using System.Text;

namespace TachographCardStructure.Types
{
    public class VehicleRegistrationNumber: IToByte, IGetLength
    {
        public byte CodePage { get; private set; } = 0x00;
        public byte[] VehicleRegNumber { get; private set; } = new byte[13];
        private Encoding _enc { get; set; }

        public VehicleRegistrationNumber(byte codePage, string regNumber) 
        {
            if (codePage != 0x00) SetCodePage(codePage);

            for (var i = 0; i < VehicleRegNumber.Length; i++) VehicleRegNumber[i] = 0x20;

            if (regNumber != "") SetText(regNumber);
        }

        public void SetCodePage(byte codePage) {
            CodePage = codePage;
            _enc = CodePages.GetEncoding(codePage);
        }

        public void SetText(string regNumber)
        {
            var utf8enc = Encoding.UTF8;
            var bytes = utf8enc.GetBytes(regNumber);
            var en_bytes = Encoding.Convert(utf8enc, _enc, bytes);
            var size = en_bytes.Length > 13 ? 13 : en_bytes.Length;
            Array.Copy(en_bytes, VehicleRegNumber, size);
        }

        public override string ToString()
        {
            return _enc.GetString(VehicleRegNumber);
        }

        public byte[] ToByte()
        {
            byte[] t_arr = new byte[GetLength()];
            t_arr[0] = CodePage;
            Array.Copy(VehicleRegNumber, 0, t_arr, 1, VehicleRegNumber.Length);

            return t_arr;
        }

        public ushort GetLength()
        {
            return 14;
        }
    }
}