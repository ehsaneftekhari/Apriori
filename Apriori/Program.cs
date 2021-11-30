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
