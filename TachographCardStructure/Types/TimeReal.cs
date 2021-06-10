using System;
using System.Linq;

namespace TachographCardStructure.Types
{
    public class TimeReal : IToByte, IGetLength
    {
        private readonly static DateTime baseTime = new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        private UInt32 ticks;

        public TimeReal(UInt32 ticks)
        {
            this.ticks = ticks;
        }
        public TimeReal(DateTime date)
        {
            var diff = date - baseTime;
            ticks = (UInt32)diff.TotalSeconds;
        }

        public override string ToString()
        {
            return baseTime.AddSeconds(ticks).ToString();
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return baseTime.AddSeconds(ticks).ToString(format, provider);
        }

        public string ToString(string format)
        {
            return baseTime.AddSeconds(ticks).ToString(format);
        }

        public ushort GetLength()
        {
            return 4;
        }

        public byte[] ToByte()
        {
            var t_b = BitConverter.GetBytes(ticks);
            if (BitConverter.IsLittleEndian) t_b = t_b.Reverse().ToArray();

            return t_b;
        }
    }
}
