using System;
using System.Collections.Generic;
using System.IO;
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
            testFile();
            Console.ReadKey(); ;
            printStruct(new byte[] { }, 16);

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

        static void testFile(byte[] test, byte[] origin, byte width = 0x0F)
        {
            var ZipValues = test.Zip(origin, (t, o) => new { testValue = t, orginValue = o });

            byte col = 0;
            uint row = 0;

            ulong errorCount = 0;

            foreach (var pair in ZipValues)
            {

                if (pair.testValue != pair.orginValue)
                {
                    errorCount++;
                    var tv_s = BitConverter.ToString(new byte[] { pair.testValue });
                    var ov_s = BitConverter.ToString(new byte[] { pair.orginValue });
                    Console.Write($"test value: {tv_s}, orginal value: {ov_s}\tpos: [");
                    Console.WriteLine("{0:X2}, {1:X8}]", col, row);
                }
                col++;
                if (col > width)
                {
                    row++;
                    col = 0x00;
                }
            }

            Console.WriteLine('\n');

            if (errorCount == 0)
            {
                Console.WriteLine("Struktura pliku jest taka sama");
            }
            else
            {
                Console.WriteLine("Pliki róznią się w " + errorCount + " miejscach");
            }

            Console.WriteLine("Ilośc sprawdzonych bytów: " + ZipValues.Count());

        }

        static void testFile(string src = @"C:\Users\Pan Patryk\Downloads\C_20210423_115927_M_Kowalskii_11111111110000")
        {
            var test = (new TestFile()).ToByte();
            var orgin = File.ReadAllBytes(src);
            testFile(test, orgin);
        }


        static void printStruct(byte[] bytes, int width = 8)
        {
            if(bytes.Length == 0)
            {
                var t = new TestFile();
                bytes = t.ToByte();
            }

            var str = new StringBuilder();
            uint row = 0;
            str.AppendFormat("{0,12}", "");
            for(byte b = 0; b< width; b++ )
            {
                str.AppendFormat("{0:X2} ", b);
            }
            str.AppendLine();
            str.AppendLine();
            str.AppendFormat("{0,8:X8}    ", row++); 

            for (int i = 0; i < bytes.Length; i++)
            {
                str.AppendFormat("{0:X2} ", bytes[i]);
                if ((i + 1) % width == 0 && i != 0)
                {
                    str.AppendLine();
                    str.AppendFormat("{0,8:X8}    ", row++);

                }

            }

            Console.WriteLine(str.ToString());

        }
    }

}
