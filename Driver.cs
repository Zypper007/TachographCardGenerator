using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_pliku_ddd
{
    public class Driver
    {
        public class Person
        {
            
            public Person(string FirstName, string LastName)
            {
                var rand = RandomSingleton.GetInstance();
                var _23_years_old = DateTime.Now.AddYears(-22);
                var _65_years_old = DateTime.Now.AddYears(-64);

                var diff = (_23_years_old - _65_years_old).Days;

                BirthDay = _65_years_old.AddDays(rand.Next(diff));
                ID = $"{BirthDay.ToString("yyMMdd")}12345";
                this.FirstName = FirstName;
                this.LastName = LastName;
            }

            public Person(string firstName, string lastName, string iD, DateTime birthDay)
            {
                FirstName = firstName;
                LastName = lastName;
                ID = iD;
                BirthDay = birthDay;
            }

            public readonly string FirstName;
            public readonly string LastName;
            public readonly string ID;
            public readonly DateTime BirthDay;
        }

        public class DrivingLicense
        {

            public DrivingLicense()
            {
                var rand = RandomSingleton.GetInstance();
                IssuingAuthority = IssuingAuthorites[rand.Next(0, IssuingAuthorites.Length - 1)];
                Serial = rand.Next(123, 99999).ToString("D5") + "/" + rand.Next(0, 99).ToString("D2") + "/" + rand.Next(0, 9999).ToString("D4");
            }

            public DrivingLicense(string issuingAuthority, string serial)
            {
                IssuingAuthority = issuingAuthority;
                Serial = serial;
            }

            private static string[] IssuingAuthorites = new string[] {
                "Prezydent Miasta Chełm",
                "Prezydent Miasta Wrocław",
                "Prezydent Miasta Bielska Białej",
                "Prezydent Miasta Warszawa",
                "Prezydent Miasta Kraków",
                "Prezydent Miasta Zagrzeb",
                "Prezydent Miasta Szczecin",
                "Prezydent Miasta Narni",
                "Prezydent Miasta Nysy"
            };
            public readonly string IssuingAuthority;
            public readonly string Serial;
        }

        public class DriverCard
        {

            public DriverCard()
            {
                var rand = RandomSingleton.GetInstance();
                long n = rand.Next(int.MinValue, int.MaxValue);
                if (n < 0) n = n * -2;
                ExtendedSerialNumber = (uint)n;
                n = rand.Next(int.MinValue, int.MaxValue);
                if (n < 0) n = n * -2;
                SerialNumber = (uint)n;
                n = rand.Next(int.MinValue, int.MaxValue);
                if (n < 0) n = n * -2;
                ManufacturingReferences = (uint)n;
                var ReleaseDiff = rand.Next(0, (int)(DateTime.Now - DateTime.Now.AddYears(-5)).TotalDays);
                ReleaseDate = DateTime.Now.AddDays(-ReleaseDiff);
                ExpiredDate = ReleaseDate.AddYears(5);
                Company = Companies[rand.Next(0, Companies.Length - 1)];
            }

            public DriverCard(uint extendedSerialNumber, uint serialNumber, uint manufacturingReferences, DateTime releaseDate, DateTime expiredDate, string approvalCode, string company)
            {
                ExtendedSerialNumber = extendedSerialNumber;
                SerialNumber = serialNumber;
                ManufacturingReferences = manufacturingReferences;
                ReleaseDate = releaseDate;
                ExpiredDate = expiredDate;
                ApprovalCode = approvalCode;
                Company = company;
            }

            public readonly uint ExtendedSerialNumber;
            public readonly uint SerialNumber;
            public readonly uint ManufacturingReferences;
            public readonly DateTime ReleaseDate;
            public readonly DateTime ExpiredDate;
            public readonly string ApprovalCode = "XXXXXXXX";
            public readonly string Company;

            private static string[] Companies = new string[]
            {
                "JanszPol P.W.",
                "WiertPol P.W.",
                "GruzPol P.W.",
                "Pudzian P.W."
            };
        }


        public Person Personals { get; private set; }
        public DrivingLicense DrivingLicenseInfo { get; private set; }
        public DriverCard DriverCardInfo { get; private set; }

        public Driver(string FirstName, string LastName)
        {
            Personals = new Person(FirstName, LastName);
            DrivingLicenseInfo = new DrivingLicense();
            DriverCardInfo = new DriverCard();
        }

        public Driver(string CSV)
        {
            var data = CSV.Split(';');
            Personals = new Person(data[0], data[1], data[2], DateTime.Parse(data[3]));
            DrivingLicenseInfo = new DrivingLicense(data[4], data[5]);
            DriverCardInfo = new DriverCard(uint.Parse(data[6]), uint.Parse(data[7]), uint.Parse(data[8]), DateTime.Parse(data[9]), DateTime.Parse(data[10]), data[11], data[12]);
        }

        public string toCSV (char separator = ';')
        {
            return Personals.FirstName +
                 separator +
                 Personals.LastName +
                 separator +
                 Personals.ID +
                 separator +
                 Personals.BirthDay.ToString("d") +
                 separator +
                 DrivingLicenseInfo.IssuingAuthority +
                 separator +
                 DrivingLicenseInfo.Serial +
                 separator +
                 DriverCardInfo.ExtendedSerialNumber +
                 separator +
                 DriverCardInfo.SerialNumber +
                 separator +
                 DriverCardInfo.ManufacturingReferences +
                 separator +
                 DriverCardInfo.ReleaseDate.ToString("d") +
                 separator +
                 DriverCardInfo.ExpiredDate.ToString("d") +
                 separator +
                 DriverCardInfo.ApprovalCode +
                 separator +
                 DriverCardInfo.Company;
        }
    }
}
