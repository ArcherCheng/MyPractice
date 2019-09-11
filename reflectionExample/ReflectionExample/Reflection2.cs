using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample
{
    public class Reflection2
    {
        public static void DoReflection1()
        {
            int val = 100;
            var type1 = val.GetType();
            Console.WriteLine($" val.GetType() = {type1}"); //System.Int32
            var type2 = val.GetTypeCode();
            Console.WriteLine($" val.GetTypeCode() = {type2}"); //Int32
            var type3 = typeof(int);
            Console.WriteLine($" typeof(int) = {type3}"); //System.Int32
            Console.ReadLine();
        }

        public static void DoReflection2()
        {
            var myObj = new MyClass();
            var type = typeof(MyClass);
            var value1 = type.GetProperty("UserId");
            var value2 = type.GetProperties();
            var value3 = type.GetMethods();
            var value4 = type.GetFields();

            Console.WriteLine(value1);
            Console.WriteLine(value2);
            Console.ReadLine();
        }
    }
}
