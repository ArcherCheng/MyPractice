using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample
{
    public class MyClass
    {
        public int UserId { get; set; }
        internal int Sex { get; set; }
        private int Marry { get; set; }
        protected int Heights { get; set; }

        public int Weights = 60;
        internal int salary = 100;
        private int payAmt = 30;
        protected int products = 300;

        public string NickName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsCloseData { get; set; }
        public bool IsClosePhoto { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? LoginDate { get; set; }
        public DateTime? ActiveDate { get; set; }

        public void SayHello()
        {
            Console.WriteLine("Hello World!");
        }

        public void SayHi(string message)
        {
            Console.WriteLine($"Hi, {message}");
        }

        protected void ShowMessage(string message)
        {
            Console.WriteLine($"Send Message: {message}");
        }

        private void SaySomething(string message)
        {
            Console.WriteLine($"Say Somthing: {message}");
        }
    }
}
