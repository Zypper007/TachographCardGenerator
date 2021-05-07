using System;
using System.Collections.Generic;
using System.Text;

namespace api.dane.gov.pl
{   
    // klasa zawiera punkty koncowe zapytan api

    internal static class Endpoints
    {
        private static string BaseUrl = "https://api.dane.gov.pl/";
        public static string ManNames = BaseUrl + "resources/28100/data";
        public static string WomanNames = BaseUrl + "resources/28099/data";
        public static string ManLastname = BaseUrl + "resources/28091/data";
        public static string WomanLastname = BaseUrl + "resources/28087/data";
    }
}
