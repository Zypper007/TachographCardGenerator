using System;
using System.Collections.Generic;
using System.Text;

namespace TachographCardStructure.Types
{
    static class ClockStops
    {
        public const byte ALLOWED = 0b_0000_0001;
        public const byte ALLOWED_PREF_HIGHT_LVL = 0b_0000_0011;
        public const byte ALLOWED_PREF_LOW_LVL = 0b_0000_0101;
        public const byte NOT_ALLOWED = 0b_0000_0000;
        public const byte ALLOWED_ONLY_HIGHT_LVL = 0b_0000_0010;
        public const byte ALLOWED_ONLY_LOW_LVL = 0b_0000_0100;
    }
}
