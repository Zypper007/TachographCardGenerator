using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachographCardStructure.Types;

namespace Tests
{
    class ClassB : ClassA
    {
        public int pro1 { get; set; } = 1;
        public int pro2 { get; set; } = 2;
        public int pro3 { get; set; } = 3;
        public IA5String str { get; set; } = new IA5String(2);
        public ClassB()
        {
            str.SetText("AB");
        }
    }
}
