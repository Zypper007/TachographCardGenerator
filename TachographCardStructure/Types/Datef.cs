using System;
using System.Collections.Generic;
using System.Linq;

namespace TachographCardStructure.Types
{
    public class Datef : IToByte, IGetLength
    {
        public BCDString Year { get; set; } = new BCDString(2);
        public BCDString Month { get; set; } = new BCDString(1);
        public BCDString Day { get; set; } = new BCDString(1);

        public Datef(string year, string month, string day)
        {
            Year.SetNumber(year);
            Month.SetNumber(month);
            Day.SetNumber(day);
        }

        public Datef(int year, int month, int day)
        {
            Year.SetNumber(year);
            Month.SetNumber(month);
            Day.SetNumber(day);
        }

        public Datef(DateTime date)
        {
            var y = date.Year;
            var m = date.Month;
            var d = date.Day;

            Year.SetNumber(y);
            Month.SetNumber(m);
            Day.SetNumber(d);
        }


        public ushort GetLength()
        {
            return (ushort)(Year.GetLength()
                    + Month.GetLength()
                    + Day.GetLength());
        }

        public byte[] ToByte()
        {
            return new List<byte>()
                .Concat(Year.ToByte())
                .Concat(Month.ToByte())
                .Concat(Day.ToByte())
                .ToArray();
        }
    }
}
