using System;
using System.Collections.Generic;
using System.Text;

namespace TachographCardStructure.Types
{
    static public class CodePages { 
        public const byte ISO_8859_1 = 1; // Lain1 
        public const byte ISO_8859_2 = 2; // Lain2 
        public const byte ISO_8859_3 = 3; // Lain3 
        public const byte ISO_8859_5 = 5; // Lain, cyrlica
        public const byte ISO_8859_7 = 7; // Lain, greka
        public const byte ISO_8859_9 = 9; // Lain5, tureck
        public const byte ISO_8859_13 = 13; // Lain7
        public const byte ISO_8859_15 = 15; // Lain9
        public const byte ISO_8859_16 = 16; // Lain10
        public const byte KOI8_R = 80;
        public const byte KOI8_U = 85;

        public static Encoding GetEncoding(byte b)
        {
            switch (b)
            {
                case 1: return Encoding.GetEncoding(28591);
                case 2: return Encoding.GetEncoding(28592);
                case 3: return Encoding.GetEncoding(28593);
                case 5: return Encoding.GetEncoding(28595);
                case 7: return Encoding.GetEncoding(28597);
                case 8: return Encoding.GetEncoding(28598);
                case 9: return Encoding.GetEncoding(28599);
                case 13: return Encoding.GetEncoding(28603);
                case 15: return Encoding.GetEncoding(28605);
                case 16: return Encoding.GetEncoding(28606); // windows not supported
                case 80: return Encoding.GetEncoding(20866); 
                case 85: return Encoding.GetEncoding(21866); 
                

                default: throw new Exception("[CodePage::GetEncoding]: bed value");
            }
        }
    }
}
