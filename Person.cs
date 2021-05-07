using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_pliku_ddd
{
    class Person
    {
        public Person(string name, string lastname, Random rand = null)
        {
            rand = rand is Random ? rand : new Random();

            var _23_years_old = DateTime.Now.AddDays(- 22 * 366);
            var _65_years_old = DateTime.Now.AddDays(-64 * 366);

            var diff =  (_23_years_old - _65_years_old).Days;

            var birthDay = _65_years_old.AddDays(rand.Next(diff));


            PESEL = $"{birthDay.ToString("yyMMdd")}12345";

            Name = name;
            Lastname = lastname;

            Debug.WriteLine(this.toCSV(' '));
        }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PESEL { get; set; }

        public override string ToString()
        {
            return $"{Name} {Lastname}";
        }

        public string toCSV (char separator = ';')
        {
            return Name + separator + Lastname + separator + PESEL;
        }
    }
}
