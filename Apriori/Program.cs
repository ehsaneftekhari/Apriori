using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyApriori_Test();
            //SubSet_Test();
        }

        static void MyApriori_Test()
        {
            MyItem i1 = new MyItem("i1");
            MyItem i2 = new MyItem("i2");
            MyItem i3 = new MyItem("i3");
            MyItem i4 = new MyItem("i4");
            MyItem i5 = new MyItem("i5");
            MyItem i6 = new MyItem("i6");
            MyItem i7 = new MyItem("i7");
            MyItem i8 = new MyItem("i8");
            MyItem i9 = new MyItem("i91");
            MyItem i10 = new MyItem("i10");
            MyItem i11 = new MyItem("i11");
            Transaction<MyItem> T1 = new Transaction<MyItem>(new MyItem[] { i1, i2, i3, i4, i5, i6 });
            Transaction<MyItem> T2 = new Transaction<MyItem>(new MyItem[] { i2, i3, i4, i5, i6, i7 });
            Transaction<MyItem> T3 = new Transaction<MyItem>(new MyItem[] { i1, i4, i5, i8 });
            Transaction<MyItem> T4 = new Transaction<MyItem>(new MyItem[] { i1, i4, i6, i9, i10 });
            Transaction<MyItem> T5 = new Transaction<MyItem>(new MyItem[] { i2, i4, i5, i10, i11 });

            MyApriori<MyItem> Ap = new MyApriori<MyItem>();
            Ap.MinSupport = 0.6;
            Ap.MinConfident = 0.6;
            Ap.AddTransaction(T1);
            Ap.AddTransaction(T2);
            Ap.AddTransaction(T3);
            Ap.AddTransaction(T4);
            Ap.AddTransaction(T5);
            Ap.Process();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Frequent Sets:");
            Console.ResetColor();
            foreach (List<Set<MyItem>> FrequentSet_Level_List in Ap.FrequentSets)
            {
                foreach (Set<MyItem> set in FrequentSet_Level_List)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("(");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(set.InString);

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" S=" + (Ap.GetSupportOf(set) * 100) + "%");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(")");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(", ");
                }
                Console.WriteLine("\n");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Rules:");
            Console.ResetColor();
            foreach (Rule<MyItem> rule in Ap.Rules)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(rule.InString);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(" C=" + (Ap.GetConfidentOf(rule) * 100) + "%");
            }
            Console.ReadLine();
        }

        static void SubSet_Test()
        {
            Set<MyItem> set = new Set<MyItem>();
            set.Add(new MyItem("i1"));
            set.Add(new MyItem("i2"));
            set.Add(new MyItem("i3"));
            set.Add(new MyItem("i4"));

            List<Set<MyItem>> Subsets = new List<Set<MyItem>>(set.SubSets());
            foreach (Set<MyItem> subset in Subsets)
                subset.Print();
            Console.WriteLine(Subsets.Last().IsEqual(set));
            Console.WriteLine("*");
            Console.ReadLine();
        }

       
    }
}
