using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TachographCardStructure
{
    abstract public class MainFile: IToByte, IGetLength
    {

        public byte[] ID { get; private set; } = new byte[2];
        public byte Encrypted { get; private set; }
        public byte[] EncryptedData { get; protected set; }

        public MainFile(byte[] id)
        {
            ID[0] = id[0];
            ID[1] = id[1];
            Encrypted = 0x00;
        }

        public MainFile(byte[] id, byte[] encryptedData)
        {
            ID[0] = id[0];
            ID[1] = id[1];
            Encrypted = 0x01;
            EncryptedData = new byte[encryptedData.Length];
            Array.Copy(encryptedData, EncryptedData, EncryptedData.Length);
        }
        
        virtual public ushort GetLength()
        {
            return (ushort)EncryptedData.Length;
        }

        virtual public byte[] ToByte()
        {
            var length = BitConverter.GetBytes(GetLength());
            if (BitConverter.IsLittleEndian) length = length.Reverse().ToArray();

            var bytes = new List<byte>()
                .Concat(ID)
                .Append(Encrypted)
                .Concat(length);

            if (Encrypted == 0x01)
                bytes = bytes.Concat(this.EncryptedData);

            return bytes.ToArray();
        }

        public static implicit operator byte[](MainFile mf) => mf.ToByte();
    }
}
