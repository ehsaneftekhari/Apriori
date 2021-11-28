using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class Transaction<T> where T : IItem
    {
        private List<T> itemsList;
        public T this[int i]
        {
            get { return itemsList.ElementAt(i); }
        }
        public string InString
        {
            get
            {
                string str = "[";
                foreach (T t in itemsList)
                {
                    str += t.InString + ", ";
                }
                str += "]";
                return str;
            }
        }

        public Transaction()
        {
            itemsList = new List<T>();
        }
        public Transaction(T Item) : this()
        {
            itemsList.Add(Item);
        }
        public Transaction(IEnumerable<T> Items) : this()
        {
            itemsList.AddRange(Items);
        }
        public void AddItem(T Item)
        {
            itemsList.Add(Item);
        }
        public void Print()
        {
            Console.WriteLine(InString);
        }
    }
}
