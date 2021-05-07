using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachographCardStructure;
using TachographCardStructure.Types;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new ClassB();

            b.iterate();

            Console.ReadKey();
        }

        static void LoopCvr()
        {
            for (; ; )
            {
                var x = Convert.ToUInt16(Console.ReadLine());
                var xb = BitConverter.GetBytes(x);
                Console.WriteLine(BitConverter.ToString(xb));
                xb = xb.Reverse().ToArray();
                Console.WriteLine(BitConverter.ToString(xb));
                x = BitConverter.ToUInt16(xb, 0);
                Console.WriteLine(x);
            }
        }

        static void testFile()
        {
            var t = new TestFile();

            var bytes = t.ToByte();

            var str = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                str.AppendFormat("{0:x2} ", bytes[i]);
                if ((i + 1) % 8 == 0 && i != 0) str.AppendLine();
            }

            Console.WriteLine(str.ToString());

        }
    }

}
