using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExample
{
    /// <summary>
    /// 此為練習 反射取得物件(.dll,.exe...etc)屬性方法或其他資訊
    /// </summary>
    public class Reflection3
    {
        // 組件(Assembly)包含模組(Module)，模組包含型別(Type)，型別包含成員。或換個角度來想，一個Class (類別)裡有什麼東西？
        public static void DoReflection()
        {
            // 反射有以下三種方式 1.字串 2.類別 3.實例物件
            var t1 = Assembly.Load("ConsoleApp1").GetType("ConsoleApp1.TargetPerson");
            Console.WriteLine(t1.ToString());

            var t2 = typeof(TargetPerson);
            Console.WriteLine(t2.ToString());
            var t2Instance = Activator.CreateInstance(t2); // 實例化類別(類型)

            var ob3 = new TargetPerson();
            var t3 = ob3.GetType();
            var t3Instance = Activator.CreateInstance(t3); // 實例化類別(類型)
            Console.WriteLine(ob3.ToString());
            Console.WriteLine(t3Instance.ToString());



            //// 由實體.dll,.exe...etc檔案結構獲取相關屬性 Assembly
            //Assembly assembly1 = Assembly.
            //    LoadFrom(@"C:\CSharpStudy\ReflectionTraning\ReflectionPractice\ReflectionTargetOb\bin\Debug\ReflectionTargetOb.dll");
            //// Assembly assembly2 = Assembly.ReflectionOnlyLoad 若只要取得相關資訊，可使用此方法增加程式效能
            //Console.WriteLine(assembly1.GetType("ReflectionTargetOb.TargetPerson").ToString());
            //var assembly1Instance = assembly1.CreateInstance("ReflectionTargetOb.TargetPerson", true);
            ///*
            //    使用 Assembly 類可取得物件或組件內部相關的資訊,有三種方式
            //    1. Assembly.LoadFrom() - 針對路徑作加載的動作，並取得物件的屬性(有可能會發生例外)
            //    2. Assembly.Load() - 取得已引入的組件的相關資訊
            //    3. Assembly.LoadFile() - 針對路徑作加載的動作，並取得物件的屬性但並不會取得該物件自身引入(依賴)的組件
            // */

            //Module module1 = assembly1.GetModules()[0]; // 將物件的屬性..等資料,轉換成 Module
            //Console.WriteLine(module1.GetType("ReflectionTargetOb.TargetPerson").ToString());

            //// 執行時期取得物件資訊
            ReflectionObMethod(t2);

            //// 執行時期取得物件欄位資訊
            //ReflecationObProperty(t2);

            //// 執行時期使用指定物件名稱,設定屬性並執行相關方法
            //SimpleReflection("ReflectionTargetOb", "TargetPerson");

            //// 使用 Dictionary 當作資料，動態取得物件欄位並賦予值
            //ReflectionDataIn();

            //// 通過System.Reflection.FieldInfo 能查找到類裡面私有或公開的屬性
            //ReflectionFieldInfo("ReflectionTargetOb", "TargetPerson");

            //// 取得類型中欄位所擁有的特性(Attribute)
            //ReflectionCustomAttributs("ReflectionTargetOb", "EntityData1");

            Console.ReadKey();









        }

        // 取得物件執行時期的相關方法, 經測試取得物件所有公開屬性與方法外, 還取得.Net原生配給給Object的公開屬性及方法
        public static void ReflectionObMethod(Type type)
        {
            MethodInfo[] methodInfos = type.GetMethods();
            Console.WriteLine();
            Console.WriteLine("使用MethodInfo裝載物件資訊:");
            foreach (var methodInfo in methodInfos)
            {
                Console.WriteLine(methodInfo);
            }
            Console.WriteLine("依據方法名稱(字串)取得該方法的中繼資料");
            MethodInfo methodShow = type.GetMethod("Show"); // 依據方法名稱取得該方法的中繼資料
            methodShow.Invoke(Activator.CreateInstance(type), null); // 調用方法並執行
            Console.WriteLine("-------------------------------");

        }

        public static void ReflectionObProperty(Type type)
        {
            PropertyInfo[] propertyInfos = type.GetProperties();
            Console.WriteLine();
            Console.WriteLine("使用PropertyInfo裝載物件資訊: " + type.Name + "---- - ");
            foreach (var propertyInfo in propertyInfos)
            {
                Console.WriteLine(propertyInfo);
            }


            PropertyInfo[] PropertyInfoList = type.GetProperties();
            Console.WriteLine();
            Console.WriteLine("使用PropertyInfo裝載物件資訊: " + type.Name + " -----");
            foreach (var propertyInfo in PropertyInfoList)
                Console.WriteLine(propertyInfo);
            Console.WriteLine("使用 SetValue設定物件屬性, GetProperty 取得物件屬性");
            PropertyInfo typePropertyInfo = type.GetProperty("Name"); // 依據屬性名稱取得屬性的中繼資料
            var ob1 = Activator.CreateInstance(type); // 使用類型實例化
            typePropertyInfo.SetValue(ob1, "設定屬性值"); // 使用欄位資訊設定 ob1 相對應的屬性
            Console.WriteLine("使用GetValue取值: " + typePropertyInfo.GetValue(ob1)); // 使用欄位資訊取得 ob1 相對應的屬性
            Console.WriteLine("-------------------------------");

        }
    }
}
