using System;
using System.Collections.Generic;
using System.Text;

namespace TachographCardStructure.Types
{
    public class EquipmentType : IToByte, IGetLength
    {
        public static EquipmentType Reserved = new EquipmentType(0);
        public static EquipmentType DriverCard = new EquipmentType(1);
        public static EquipmentType WorkshopCard = new EquipmentType(2);
        public static EquipmentType ControlCard = new EquipmentType(3);
        public static EquipmentType CompanyCard = new EquipmentType(4);
        public static EquipmentType ManufacturingCard = new EquipmentType(5);
        public static EquipmentType VehicleUnit = new EquipmentType(6);
        public static EquipmentType MotionSensor = new EquipmentType(7);
        public static EquipmentType GNSS_Facility = new EquipmentType(8);
        public static EquipmentType RemoteCommunicationModule = new EquipmentType(9);
        public static EquipmentType ITS_InterfaceModule = new EquipmentType(10);
        public static EquipmentType Plaque = new EquipmentType(11);
        public static EquipmentType M1N1_Adapter = new EquipmentType(12);
        public static EquipmentType EuropenRoot_CA_ERCA = new EquipmentType(13);
        public static EquipmentType MemberState_CA_MSCA = new EquipmentType(14);
        public static EquipmentType External_GNSS_Connection = new EquipmentType(15);
        public static EquipmentType Unused = new EquipmentType(16);

        public EquipmentType(byte b)
        {
            Value = b;
        }

        public EquipmentType(byte value, string tag)
        {
            Value = value;
            Tag = tag;
        }

        public override string ToString()
        {
            switch(Value)
            {
                case 0: return "Reserved";
                case 1: return "Driver Card";
                case 2: return "Workshop Card";
                case 3: return "Control Card";
                case 4: return "Company Card";
                case 5: return "Manufacturing Card";
                case 6: return "Vehicle Unit";
                case 7: return "Motion Sensor";
                case 8: return "GNSS Facility";
                case 9: return "Remote Communication Module";
                case 10: return "ITS Interface Module";
                case 11: return "Plaque";
                case 12: return "M1/N1 Adapter";
                case 13: return "Europen Root CA (ERCA)";
                case 14: return "Member State CA (MSCA)";
                case 15: return "External GNSS Connection";
                case 16: return "Unused";
                default: return Tag;
            }
        }

        public byte Value { get; set; }
        public string Tag { 
            get
            {
                return _tag == "" ? Value.ToString() : _tag;
            }
            set
            {
                _tag = value;
            }
        }
        private string _tag;

        public byte[] ToByte()
        {
            return new byte[] { Value };
        }

        public ushort GetLength()
        {
            return 1;
        }
    }
}
