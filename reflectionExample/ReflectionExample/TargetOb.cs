using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample
{
    public class EntityData1
    {
        [My(1)]
        public int Id { get; set; }
        [My(2)]
        public string Name { get; set; }
        [My(3)]
        public DateTime Birthday { get; set; }
    }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class MyAttribute : Attribute
    {
        readonly int _NamedInt;

        public MyAttribute(int NamedInt)
        {
            _NamedInt = NamedInt;
        }
        public int NamedInt
        {
            get { return _NamedInt; }
        }
    }

    public class TargetPerson
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public void Show()
        {
            Console.WriteLine("我是 TargetOb.Show()");
        }

        public void Say()
        {
            Console.WriteLine("我的名字叫{0},今年{0}歲", this.Name, this.Age);
        }

        public void Add(int i, int j)
        {
            Console.WriteLine(i + j);
        }

        private void _Method1()
        {

        }

    }
}
