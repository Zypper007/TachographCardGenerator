using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_pliku_ddd
{
    class RandomSingleton : Random
    {
        private static RandomSingleton _instance;
        private RandomSingleton() : base(){}

        public static RandomSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RandomSingleton();
            }

            return _instance;
        }
    }
}
