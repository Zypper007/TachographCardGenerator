using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_pliku_ddd
{
    class DriverEventArgs : EventArgs
    {
        public readonly Driver Driver;
        public DriverEventArgs(Driver d = null) : base()
        {
            Driver = d;
        }
    }
}
