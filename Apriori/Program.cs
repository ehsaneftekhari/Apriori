using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    internal class Program
    {
        static void Main(string[] args)
        {
            program();
        }
        static void program()
        {
            while (true)
            {
                Console.WriteLine("1.Read data from file\n2.StaticTest1\n3.StaticTest2\n4.random data generate");
                bool rf = true;
                char ch = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (ch)
                {
                    case '1':
                        ReadFromFileTest();
                        break;
                    case '2':
                        StaticTest1();
                        break;
                    case '3':
                        StaticTest2();
                        break;
                    case '4':
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Press a key...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        static void StaticTest1()
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
            List<Transaction<MyItem>> transactions = new List<Transaction<MyItem>>();
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i1, i2, i3, i4, i5, i6 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i2, i3, i4, i5, i6, i7 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i1, i4, i5, i8 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i1, i4, i6, i9, i10 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i2, i4, i5, i10, i11 }));
            MyApriori_Test(transactions, 0.6, 0.6);
            //SubSet_Test();
        }
        static void StaticTest2()
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
            MyItem i12 = new MyItem("i12");
            MyItem i13 = new MyItem("i13");
            MyItem i14 = new MyItem("i14");
            MyItem i15 = new MyItem("i15");
            MyItem i16 = new MyItem("i16");
            MyItem i17 = new MyItem("i17");
            MyItem i18 = new MyItem("i18");
            MyItem i19 = new MyItem("i19");
            MyItem i20 = new MyItem("i20");
            List<Transaction<MyItem>> transactions = new List<Transaction<MyItem>>();
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i1, i2, i3, i4, i5, i6 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i2, i3, i4, i5, i6, i7, i15 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i1, i4, i5, i8, i15 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i1, i4, i6, i9, i10 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i2, i4, i5, i10, i11, i15 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i2, i4, i5, i19, i11, i15 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i2, i3, i5, i18, i11, i16 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i2, i4, i5, i20, i11, i15 }));
            transactions.Add(new Transaction<MyItem>(new MyItem[] { i7, i3, i5, i10, i11, i20 }));
            MyApriori_Test(transactions, 0.6, 0.6);
        }

        static void ReadFromFileTest()
        {
            Set<MyItem> items = new Set<MyItem>();
            List<Transaction<MyItem>> transactions = new List<Transaction<MyItem>>();

            double MinSupport = 0;
            double MinConfident = 0;
            string fileAddress = "";
            string datafileAddress = "AprioriData.txt";
            bool f = true;
            if (File.Exists(datafileAddress) && File.ReadAllText(datafileAddress) != "")
            {
                fileAddress = File.ReadAllLines(datafileAddress)[0];
                Console.WriteLine("Use resent file?(press 'y' for yes 'n' for no)\n" + fileAddress);
                Console.CursorVisible = false;
                do
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.N:
                            f = false;
                            fileAddress = "";
                            break;
                        case ConsoleKey.Y:
                            f = false;
                            break;
                    }
                } while (f);
                f = true;
                Console.CursorVisible = true;
            }
            Console.Clear();
            do
            {
                if (fileAddress == "")
                {
                    Console.WriteLine("Enter new Address:");
                    fileAddress = Console.ReadLine();
                }
                if (File.Exists(fileAddress))
                {
                    f = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("file is not exists");
                    fileAddress = "";
                }
            } while (f);

            File.WriteAllText(datafileAddress, fileAddress);
            string[] lines = File.ReadAllLines(fileAddress);
            List<int> transactionsLines = new List<int>();
            List<int> itemLines = new List<int>();
            int flag = 0; //0:none 1:ItemsList 2:Transactions 3:MinSupport 4:MinConfident
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (flag == 0)
                {
                    switch (line)
                    {
                        case "ItemsList":
                            flag = 1;
                            break;
                        case "Transactions":
                            flag = 2;
                            break;
                        case "MinSupport":
                            flag = 3;
                            break;
                        case "MinConfident":
                            flag = 4;
                            break;
                    }
                }
                else
                {
                    if (line == "_" || line == "")
                        flag = 0;
                    else
                    {
                        switch (flag)
                        {
                            case 1:
                                //items.Add(new MyItem(line.Split(new char[] { ',', ' ' }, options: StringSplitOptions.RemoveEmptyEntries)));
                                itemLines.Add(i);
                                break;
                            case 2:
                                transactionsLines.Add(i);
                                break;
                            case 3:
                                MinSupport = double.Parse(line);
                                break;
                            case 4:
                                MinConfident = double.Parse(line);
                                break;
                        }
                    }
                }
            }
            foreach (int lineNumber in itemLines)
            {
                string[] Items = lines[lineNumber].Split(new char[] { ',', ' ', '\t' }, options: StringSplitOptions.RemoveEmptyEntries);
                foreach (string Item in Items)
                {
                    items.Add(new MyItem(Item));
                }
            }
            foreach (int lineNumber in transactionsLines)
            {
                string[] TransactionItems = lines[lineNumber].Split(new char[] { ',', ' ', '\t' }, options: StringSplitOptions.RemoveEmptyEntries);
                Transaction<MyItem> transaction = new Transaction<MyItem>();
                foreach (string TransactionItem in TransactionItems)
                {
                    transaction.AddItem(new MyItem(TransactionItem));
                }
                transactions.Add(transaction);
            }

            Console.WriteLine();


            MyApriori_Test(transactions, MinSupport, MinConfident, new List<MyItem>(items.Elements));

        }
        static void MyApriori_Test(List<Transaction<MyItem>> transactions, double minSupport, double minConfident, List<MyItem> Items = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Transactions:");
            Console.ResetColor();
            foreach (Transaction<MyItem> transaction in transactions)
            {
                Console.WriteLine(transaction.InString);
            }
            Console.WriteLine();

            MyApriori<MyItem> Ap = new MyApriori<MyItem>(transactions, Items);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ItemsList:");
            Console.ResetColor();
            foreach (MyItem Item in Ap.ItemsList)
            {
                Console.Write(Item.InString+", ");
            }
            Console.WriteLine();

            Ap.MinSupport = minSupport;
            Ap.MinConfident = minConfident;
            Ap.Process();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Frequent Sets:");
            Console.ResetColor();

            foreach (List<Set<MyItem>> FrequentSet_Level_List in Ap.FrequentSetsList)
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
            Console.WriteLine("RulesList:");
            Console.ResetColor();
            foreach (Rule<MyItem> rule in Ap.RulesList)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(rule.InString);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(" C=" + (Ap.GetConfidentOf(rule) * 100) + "%");
            }
            Console.WriteLine();
            Console.ResetColor();
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
