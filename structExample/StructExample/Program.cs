using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //PolorExample.DoExample();
            ScoreExample.DoExample();
        }
    }

    #region PolorExample
    class PolorExample
    {
        public static void DoExample()
        {
            Polor p1 = new Polor(2, 30);
            Polor p2 = new Polor(3, 60);
            Polor p3 = p1 * p2;
            Console.WriteLine("極座標 P1= " + p1.ToString());
            Console.WriteLine("極座標 P2= " + p2.ToString());
            Console.WriteLine("極座標 P1*P2 = " + p3.ToString());
            Console.ReadKey();
        }
    }
    struct Polor
    {
        private int a, b;
        public Polor(int n1, int n2)
        {
            this.a = n1;
            this.b = n2;

        }

        public static Polor operator *(Polor c1, Polor c2)
        {
            Polor c3 = new Polor((c1.a * c2.a), (c1.b + c2.b));
            return c3;
        }

        public new string ToString()
        {
            string s;
            s = a + " " + b;
            return s;
        }
    }
    #endregion

    #region  ScoreExample
    class ScoreExample
    {
        public static void DoExample()
        {
            ScorePass sp1 = new ScorePass();
            sp1.Score = 88;
            sp1.Check();
            Console.WriteLine("sp1.Score{0},Result{1}", sp1.Score,sp1.Result);

            ScorePass sp2 = new ScorePass(33);
            sp2.Check();
            Console.WriteLine("sp2.Score{0},Result{1}", sp2.Score, sp2.Result);

            ScorePass sp3 = new ScorePass(133);
            sp3.Check();
            Console.WriteLine("sp3.Score{0},Result{1}", sp3.Score, sp3.Result);

            ScorePass sp4 = new ScorePass();
            sp4 = sp1;
            sp4.Check();
            Console.WriteLine("sp4.Score{0},Result{1}", sp4.Score, sp4.Result);

            sp1.Score = 50;
            sp1.Check();
            Console.WriteLine("sp1.Score{0},Result{1}", sp1.Score, sp1.Result);


            Console.ReadLine();
        }

    }

    struct ScorePass
    {
        private int score;
        private string result ;

        //建構式一定要有參數
        public ScorePass(int a)
        {
            this.score = a;
            //一定有初始化所有欄位
            this.result = "不及格";
        }

        public int Score
        {
            get { return score; }
            set
            {
                if(value >=0 && value <= 100)
                {
                    score = value;
                }
                else
                {
                    Console.WriteLine("分數錯誤");
                    this.result = "分數錯誤";
                }
            }
        }

        //唯讀成員
        public string Result
        {
            get { return result; }
        }

        public void Check()
        {
            result = "不及格";
            if (score >= 60)
            {
                result = "及格";
            }
        }

        public static int num = 5;

        public static string Estimate(int score)
        {
            if (score >= 60)
            {
                return "pass";
            }
            else
            {
                return "Done";
            }
        }




    }
    #endregion
}
