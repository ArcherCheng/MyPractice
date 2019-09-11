using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace EventExample
{
    /// <summary>
    /// event 事件的五大要素
    /// event source (object, class)
    /// event (member, event)
    /// event subscriber (object, class)
    /// event handler (member, callback function)
    /// event subscribe (訂閱=>會通知訂閱的對像)(本質上是以委託類型為基礎的約定)
    /// </summary>

    class Program
    {
        static void Main(string[] args)
        {
            //TimerExample.DoExample();

            //FromEvent.DoExample();
            //FromEvent.DoExample2();
            //FromEvent.DoExample3();

            UdfEvent.DoExample();
        }
    }

    #region Timer
    public class TimerExample
    {
        public static void DoExample()
        {
            System.Timers.Timer timer = new System.Timers.Timer();  //event source => timer
            timer.Interval = 1000;

            Boy boy = new Boy(); //event subscriber class => boy
            Girl girl = new Girl();

            //兩個物件訂閱同一個事件
            timer.Elapsed += boy.Action;  //event => timer.Elapsed
            timer.Elapsed += girl.Action;  //+= => subscribe , event handler => girl.Action
            timer.Elapsed += new ElapsedEventHandler(boy.Action); //早期做法

            timer.Start();
            Console.ReadKey();
        }
    }

    //event subscriber class
    class Boy
    {
        //event handler
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Jump!");
        }
    }

    //event subscriber class
    class Girl
    {
        //event handler
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Sing!");
        }
    }
    #endregion

    #region FormEvent
    class FromEvent
    {
        //外部訂閱
        public static void DoExample()
        {
            Form form = new Form();  //event source
            Controller controller = new Controller(form);  //event subscriber
            form.ShowDialog();
        }

        //自我訂閱及觸發
        public static void DoExample2()
        {
            MyForm form = new MyForm();  //event source
            form.Click += form.FromClick;  //一個委託 訂閱 一個事件
            form.ShowDialog();
        }

        //訂閱內部成員的事件
        public static void DoExample3()
        {
            MyForm3 form = new MyForm3();
            form.ShowDialog();
        }

    }

    //自我訂閱及觸發
    class MyForm : Form
    {
        internal void FromClick(object sender, EventArgs e)
        {
            this.Text = System.DateTime.Now.ToString();
        }
    }

    //外部訂閱
    class Controller
    {
        private Form form;

        public Controller(Form form)
        {
            if (form != null)
            {
                this.form = form; 
                this.form.Click += this.FormClicked; //event & subscribe
            }
        }

        //event handler
        private void FormClicked(object sender, EventArgs e)
        {
            this.form.Text = "Hello World";
        }
    }

    //訂閱內部成員的事件
    class MyForm3 : Form
    {
        private TextBox textBox;
        private Button button;

        public MyForm3()
        {
            this.textBox = new TextBox();
            this.button = new Button();
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.button);
            this.button.Click += this.ButtonClecked;
            this.button.Top = 50;
            this.button.Text = "Show Me Now";

        }

        private void ButtonClecked(object sender, EventArgs e)
        {
            this.textBox.Text = System.DateTime.Now.ToString();
        }
    }
    #endregion

    #region User defined event
    public class UdfEvent
    {
        public static void DoExample()
        {
            Customer customer = new Customer(); 
            Waiter waiter = new Waiter();
            customer.Order += waiter.Action;  //OrderEventHandler=>Customer,OrderEventArgs
            customer.Order2 += waiter.Action2; //EventHandler =>object,EventArgs
            customer.Action();

            ////用委派會出現以下漏洞
            ////用委派 public OrderEventHandler Order;
            //OrderEventArgs e = new OrderEventArgs();
            //e.DishName = "Manhanquanxi";
            //e.Size = "large";

            //OrderEventArgs e2 = new OrderEventArgs();
            //e2.DishName = "Manhanquanxi";
            //e2.Size = "large";

            //Customer badGuy = new Customer();
            //badGuy.Order += waiter.Action;
            //badGuy.Order.Invoke(customer, e);
            //badGuy.Order.Invoke(customer, e2);


            customer.PayBill();
            Console.ReadLine();

        }


    }

    public class OrderEventArgs:EventArgs
    {
        public string DishName { get; set; }
        public string Size { get; set; }
    }

    public delegate void OrderEventHandler(Customer customer, OrderEventArgs e);

    public class Customer
    {
        //完整事件宣告方式:
        //private OrderEventHandler orderEventHandler;
        //public event OrderEventHandler Order
        //{
        //    add
        //    {
        //        this.orderEventHandler += value;
        //    }
        //    remove
        //    {
        //        this.orderEventHandler -= value;
        //    }
        //}

        //簡化事件宣告方式:
        public event OrderEventHandler Order;
        public event EventHandler Order2; //也可以用 EventHandler 這樣宣告

        //用委派 會有漏洞危險
        //public OrderEventHandler Order;

        public double Bill { get; set; }

        public void PayBill()
        {
            Console.WriteLine("I will pay ${0}", this.Bill);
        }
           
        public void WalkIn()
        {
            Console.WriteLine("Walk in.");
        }

        public void SitDown()
        {
            Console.WriteLine( "Sit Down");
        }

        public void Think()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("let me think...");
                Thread.Sleep(300);
            }

            //完整事件宣告方式:
            //if(this.orderEventHandler != null)
            //{
            //    OrderEventArgs e = new OrderEventArgs();
            //    e.DishName = "Fish";
            //    e.Size = "large";
            //    this.orderEventHandler.Invoke(this, e);
            //}

            //簡化事件宣告方式:
            if (this.Order != null)
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = "Fish";
                e.Size = "large";
                this.Order.Invoke(this, e);
            }
            if (this.Order2 != null)
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = "Manhanquanxi";
                e.Size = "large";
                this.Order2.Invoke(this, e);
            }

            this.OnOrder("Kongpao Chicken", "small");
        }

        protected void OnOrder(string dishname,string size)
        {
            if (this.Order != null)
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = dishname;
                e.Size = size;
                this.Order.Invoke(this, e);
            }
        }

        public void Action()
        {
            // Console.ReadLine();
            WalkIn();
            SitDown();
            Think();
        }
    }

    public class Waiter
    {
        public void Action(Customer customer, OrderEventArgs e)
        {
            Console.WriteLine("I will serve you the dish: {0} size: {1}",e.DishName, e.Size );
            double price = 10;
            switch (e.Size)
            {
                case "small":
                    price = price * 0.5;
                    break;
                case "large":
                    price = price * 1.5;
                    break;
                default:
                    price = price * 0.9;
                    break;
            }

            customer.Bill += price;
        }

        internal void Action2(object sender, EventArgs e)
        {
            Customer customer = sender as Customer;
            OrderEventArgs orderInfo = e as OrderEventArgs;

            Console.WriteLine("I will serve you the dish: {0} size: {1}", orderInfo.DishName, orderInfo.Size);
            double price = 10;
            switch (orderInfo.Size)
            {
                case "small":
                    price = price * 0.5;
                    break;
                case "large":
                    price = price * 1.5;
                    break;
                default:
                    price = price * 0.9;
                    break;
            }

            customer.Bill += price;

        }
    }

    #endregion

}
