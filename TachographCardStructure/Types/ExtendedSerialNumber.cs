using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TachographCardStructure.Types
{
    // 8bytes
    public class ExtendedSerialNumber: IToByte, IGetLength
    {
        public uint SerialNumber { get; set; } // 4bytes
        public BCDString MonthYear { get; set; } = new BCDString(size: 2); // 2bytes
        public EquipmentType Type { get; set; } // 1byte
        public byte ManufacturerCode { get; set; }  // 1byte

        public ExtendedSerialNumber(uint serialNumber, string monthYear, EquipmentType type, byte manufactureCode  )
        {
            SerialNumber = serialNumber;
            MonthYear.SetNumber(monthYear);
            Type = type;
            ManufacturerCode = manufactureCode;

        }

        public byte[] ToByte()
        {
            var bytes = new List<byte>(8);
            var sn_bytes = BitConverter.GetBytes(SerialNumber);
            var my_bytes = MonthYear.ToByte();
            var t_byte = Type.ToByte();

            return bytes.Concat(sn_bytes)
                .Concat(my_bytes)
                .Concat(t_byte)
                .Append(ManufacturerCode)
                .ToArray();
        }

        // zwraca ilość bytes
        public ushort GetLength()
        {
            return (ushort)(4+MonthYear.GetLength()+Type.GetLength()+1);
        }
    }
}
