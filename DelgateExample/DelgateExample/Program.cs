using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace DelgateExample
{
    public delegate int NumberCalc(int x, int y);

    class Program
    {
        static void Main(string[] args)
        {
            //系統提供的委託 Action , Func<>
            //SystemDelegate.DoExample();

            //自定義委託
            //MyDelegate.DoExample();

            //使用模板方法,Callback
            //CallBackDelegate.DoExample();

            //Multicast
            Multicast.DoExample();

        }
    }

    #region System delegate method 1 Action

    public class TestCalss
    {
        public void PrintReport()
        {
            Console.WriteLine("I am testing delegate");
        }

        public int Add(int x, int y)
        {
            int result = 0;
            result = x + y;
            Console.WriteLine("{0}+{1}={2}", x, y, result);
            return result;
        }
        public int Sub(int x, int y)
        {
            int result = 0;
            result = x - y;
            Console.WriteLine("{0}-{1}={2}", x, y, result);
            return result;
        }
    }

    class SystemDelegate
    {
        public static void DoExample()
        {
            TestCalss testClass = new TestCalss();

            Console.WriteLine("\n直接調用方法");
            testClass.PrintReport(); //直接調用方法

            //System delegate method 1 Action
            Console.WriteLine("\n間接調用方法1");
            Console.WriteLine("System delegate method 1 Action");

            Action action = new Action(testClass.PrintReport); //不可以加(),加()是執行,
            action.Invoke(); //間接調用方法
            action(); ////間接調用方法

            Type t1 = typeof(Action);
            Console.WriteLine("Action.IsClass = {0}", t1.IsClass);

            Console.WriteLine("\n間接調用方法2");
            Console.WriteLine("System delegate method 2 Func<>");
            //System delegate method 2 Func<>
            int x = 100;
            int y = 200;
            int z = 0;

            Func<int, int, int> func1 = new Func<int, int, int>(testClass.Add);
            Func<int, int, int> func2 = new Func<int, int, int>(testClass.Sub);

            z = func1(x, y);
            Console.WriteLine(z);

            z = func2(x, y);
            Console.WriteLine(z);

            Type t2 = typeof(Func<int, int, int>);
            Console.WriteLine("Func<>.IsClass = {0}", t2.IsClass);

            Console.ReadKey();
        }
    }

    #endregion


    #region 自定義委託
    //自定義委託
    public delegate double Calc(double x, double y);

    public class Calculator
    {
 
        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Sub(double x, double y)
        {
            return x - y;
        }

        public double Mul(double x, double y)
        {
            return x * y;
        }

        public double Div(double x, double y)
        {
            return x / y;
        }
    }

    public class MyDelegate
    {
        public static void DoExample()
        {
            Calculator calculator = new Calculator();
            Calc calc1 = new Calc(calculator.Add);
            Calc calc2 = new Calc(calculator.Sub);
            Calc calc3 = new Calc(calculator.Mul);
            Calc calc4 = new Calc(calculator.Div);
            double a = 100;
            double b = 200;
            double c = 0;

            Console.WriteLine("\n自定義委託 delegate double Calc(double x, double y)");
            Console.WriteLine("調用方法 calc1.Invoke(a,b)");
            c = calc1.Invoke(a, b);
            Console.WriteLine(c);
            c = calc2.Invoke(a, b);
            Console.WriteLine(c);
            c = calc3.Invoke(a, b);
            Console.WriteLine(c);
            c = calc4.Invoke(a, b);
            Console.WriteLine(c);

            Console.WriteLine("\n調用方法 calc1(a,b)");
            c = calc1(a, b);
            Console.WriteLine(c);
            c = calc2(a, b);
            Console.WriteLine(c);
            c = calc3(a, b);
            Console.WriteLine(c);
            c = calc4(a, b);
            Console.WriteLine(c);

            Console.ReadKey();
        }
    }

    #endregion


    #region 使用模板方法,callback
    public class Logger
    {
        public void Log(Product product)
        {
            Console.WriteLine("Product '{0}' created at {1}. Price is {2}",product.Name, System.DateTime.UtcNow,product.Price);
        }

    }

    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class Box
    {
        public Product Product { get; set; }
    }
    
    public class WrapFactory
    {
        //使用模板方法
        public Box WrapProduct(Func<Product> getProduct)
        {
            Box box = new Box();
            Product product = getProduct.Invoke();
            box.Product = product;
            return box;
        }

        //使用Callback方法
        public Box WrapProduct(Func<Product> getProduct, Action<Product>logCallback)
        {
            Box box = new Box();
            Product product = getProduct.Invoke();
            box.Product = product;

            if(product.Price > 50)
            {
                logCallback.Invoke(product);
            }

            return box;
        }

    }

    public class ProductFactory
    {
        public Product MakePissa()
        {
            Product product = new Product
            {
                Name = "Pizza",
                Price = 50
            };
            return product;
        }

        public Product MakeToyCar()
        {
            Product product = new Product
            {
                Name = "Toy car",
                Price = 150
            };
            return product;

        }
    }

    public interface IProductFactory
    {
        Product Make();
    }

    public class PizzaProductFactory : IProductFactory
    {
        public Product Make()
        {
            Product product = new Product()
            {
                Name = "Pizza",
                Price = 50
            };
            return product;
        }
    }

    public class ToycarProductFactory : IProductFactory
    {
        public Product Make()
        {
            Product product = new Product()
            {
                Name = "Toy car",
                Price = 150
            };
            return product;
        }
    }



    public class CallBackDelegate
    {
        //使用模板方法,回調方法
        public static void DoExample()
        {
            //Console.WriteLine("\n使用模板方法,回調方法");
            //ProductFactory productFactory = new ProductFactory();
            //WrapFactory wrapFactory = new WrapFactory();

            //Func<Product> func1 = new Func<Product>(productFactory.MakePissa);
            //Func<Product> func2 = new Func<Product>(productFactory.MakeToyCar);

            ////使用模板方法
            //Console.WriteLine("\n使用模板方法");
            //Box box1 = wrapFactory.WrapProduct(func1);
            //Box box2 = wrapFactory.WrapProduct(func2);

            //Console.WriteLine(box1.Product.Name);
            //Console.WriteLine(box2.Product.Name);

            //////使用回調方法
            //Console.WriteLine("\n使用回調方法");
            //Logger logger = new Logger();
            //Action<Product> log = new Action<Product>(logger.Log);
            //Box box3 = boxFactory.WrapProduct(func1, log);
            //Box box4 = boxFactory.WrapProduct(func2, log);
            //Console.WriteLine(box3.Product.Name, box3.Product.Price);
            //Console.WriteLine(box4.Product.Name, box4.Product.Price);

            //使用介面取代委託
            ProductFactory productFactory = new ProductFactory();
            //Wra

            Console.ReadKey();
        }

    }
    #endregion


    #region Multicast
    public class Student
    {
        public int Id { get; set; }
        public ConsoleColor PenColor { get; set; }

        public void DoHomework()
        {
            for (int i=0; i < 5; i++)
            {
                Console.ForegroundColor = this.PenColor;
                Console.WriteLine("Student {0} doing homework {1} hours", this.Id, i);
                Thread.Sleep(500);
            }
        }
    }

    public class Multicast
    {
        public static void DoExample()
        {
            Student stu1 = new Student() { Id = 1, PenColor = ConsoleColor.Yellow };
            Student stu2 = new Student() { Id = 2, PenColor = ConsoleColor.Green };
            Student stu3 = new Student() { Id = 3, PenColor = ConsoleColor.Red };


            //Console.WriteLine("\n單獨調用");
            //Action action1 = new Action(stu1.DoHomework);
            //Action action2 = new Action(stu2.DoHomework);
            //Action action3 = new Action(stu3.DoHomework);
            //action1.Invoke();
            //action2.Invoke();
            //action3.Invoke();
            ////action1();
            ////action2();
            ////action3();
            //Console.WriteLine("\n合併調用");
            //action1 += action2;
            //action1 += action3;
            //action1.Invoke();


            //Console.WriteLine("\n同步調用,直接調用");
            //stu1.DoHomework();
            //stu2.DoHomework();
            //stu3.DoHomework();
            //for(int i = 0; i < 10; i++)
            //{
            //    Console.ForegroundColor = ConsoleColor.Cyan;
            //    Console.WriteLine("Main thread {0}", i);
            //    Thread.Sleep(500);
            //}


            //Console.WriteLine("\n同步調用,間接調用");
            //Action action1 = new Action(stu1.DoHomework);
            //Action action2 = new Action(stu2.DoHomework);
            //Action action3 = new Action(stu3.DoHomework);
            //action1();
            //action2();
            //action3();
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.ForegroundColor = ConsoleColor.Cyan;
            //    Console.WriteLine("Main thread {0}", i);
            //    Thread.Sleep(500);
            //}



            //Console.WriteLine("\n隱式異步調用,間接調用");
            //Action action1 = new Action(stu1.DoHomework);
            //Action action2 = new Action(stu2.DoHomework);
            //Action action3 = new Action(stu3.DoHomework);

            //action1.BeginInvoke(null, null);
            //action2.BeginInvoke(null, null);
            //action3.BeginInvoke(null, null);

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.ForegroundColor = ConsoleColor.Cyan;
            //    Console.WriteLine("Main thread {0}", i);
            //    Thread.Sleep(500);
            //}
            

            //Console.WriteLine("\n顯示異步調用,間接調用 Thread");
            //Thread thread1 = new Thread(new ThreadStart(stu1.DoHomework));
            //Thread thread2 = new Thread(new ThreadStart(stu2.DoHomework));
            //Thread thread3 = new Thread(new ThreadStart(stu3.DoHomework));

            //thread1.Start();
            //thread2.Start();
            //thread3.Start();

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.ForegroundColor = ConsoleColor.Cyan;
            //    Console.WriteLine("Main thread {0}", i);
            //    Thread.Sleep(500);
            //}


            Console.WriteLine("\n顯示異步調用,間接調用 Task");
            Task task1 = new Task(new Action(stu1.DoHomework));
            Task task2 = new Task(new Action(stu2.DoHomework));
            Task task3 = new Task(new Action(stu3.DoHomework));
            task1.Start();
            task2.Start();
            task3.Start();
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main thread {0}", i);
                Thread.Sleep(500);
            }


            //使用介面取代委託

            Console.ReadKey();
        }
    
    }
    #endregion
}

