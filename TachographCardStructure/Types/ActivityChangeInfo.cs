using System;
using System.Collections.Generic;
using System.Text;

namespace TachographCardStructure.Types
{
    public class ActivityChangeInfo : IToByte
    {
        public enum S_Parametr
        {
            DRIVER = 0b_0000_0000,
            CO_DRIVER = 0b_1000_0000
        }

        public enum C_Parametr
        {
            ONE_DRIVER = 0b_0000_0000,
            CREW = 0b_0100_0000
        }

        public enum P_Parametr
        {
            INSERD = 0b_0000_0000,
            NOT_INSERD = 0b_0010_0000
        }

        public enum AA_Parametr
        {
            BREAK = 0b_0000_0000,
            READY = 0b_0000_1000,
            WORK = 0b_0001_0000,
            LEADING = 0b_0001_1000
        }


        public S_Parametr CARD_READER_STATE;
        public C_Parametr DRIVING_CAR_STATE;
        public P_Parametr CARD_STATE;
        public AA_Parametr ACTION_STATE;

        private int _minutes;
        public int Minutes {
            get => _minutes;
            set
            {
                if (value > 1440 || value < 0) throw new Exception("[ActivityChangeInfo:Minutes] Value over. Require value between 0 and 1440");
                _minutes = value;
            }
        }

        public ActivityChangeInfo(S_Parametr cardReaderState, C_Parametr drivingCarState, P_Parametr cardState, AA_Parametr actionState, int minutes)
        {
            CARD_READER_STATE = cardReaderState;
            DRIVING_CAR_STATE = drivingCarState;
            CARD_STATE = cardState;
            ACTION_STATE = actionState;
            Minutes = minutes;
        }

        public TimeSpan GetMinutes()
        {
            return TimeSpan.FromMinutes(Minutes);
        }

        public byte[] ToByte()
        {
            var b_minutes = BitConverter.GetBytes(Minutes);
            byte b1 = (byte)((int)CARD_READER_STATE | (int)DRIVING_CAR_STATE | (int)CARD_READER_STATE | (int)ACTION_STATE);
            b1 = (byte)(b1 | b_minutes[1]);
            return new byte[] { b1, b_minutes[0] };

        }
    }
}
