using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachographCardStructure;
using TachographCardStructure.Types;
namespace Tests
{
    abstract class ClassA
    {
        public void iterate()
        {
            Console.WriteLine(this.GetType());
            var props = this.GetType().GetProperties();
            foreach(var prop in props)
            {
                Console.WriteLine(prop);
                if (typeof(IToByte).IsAssignableFrom(prop.PropertyType))
                {
                    Console.WriteLine($"{prop.Name} iplements interface IToByte");
                    var m = prop.PropertyType.GetMethod("ToByte");
                    var o = prop.GetValue(this);
                    byte[] b = (byte[])m.Invoke(o, new object[] { });
                    Console.WriteLine(BitConverter.ToString(b));

                }

            }
        }
    }
}
