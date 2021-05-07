using System;
using System.Collections.Generic;
using System.Text;
using TachographCardStructure.Types;
using System.Linq;

namespace TachographCardStructure
{
    // 8bytes
    public class ExtendedSerialNumber: IToByte, IGetLength
    {
        public uint SerialNumber { get; set; } // 4bytes
        public BCDString MonthYear { get; set; } = new BCDString(size: 2); // 2bytes
        public EquipmentType Type { get; set; } // 1byte
        public byte ManufacturerCode { get; set; }  // 1byte

        public ExtendedSerialNumber(uint serialNumber = 222042148, int monthYear = 0115, EquipmentType type = null, byte manufactureCode = 0x89 )
        {
            SerialNumber = serialNumber;
            MonthYear.SetNumber(monthYear);
            if (type is EquipmentType) Type = type;
            else  Type = new EquipmentType(0x90);
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
