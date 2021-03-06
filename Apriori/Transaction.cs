using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Apriori
{
    class Transaction<T> where T : IItem, ICloneable<T>
    {
        private List<T> itemsList;
        public T this[int i]
        {
            get { return itemsList.ElementAt(i).Clone(); }
        }
        public IEnumerable<T> Items
        {
            get
            {
                List<T> temp = new List<T>();
                foreach (T item in itemsList) temp.Add(item.Clone());
                return temp;
            }
        }
        public string InString
        {
            get
            {
                string str = "[";
                for (int i = 0; i < itemsList.Count; i++)
                {
                    str += itemsList.ElementAt(i).InString;
                    if (i < itemsList.Count - 1)
                        str += ", ";
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
        public bool Contains(Set<T> set)
        {
            if (set == null)
                return false;

            if (set.Count > itemsList.Count)
                return false;

            for (int i = 0; i < set.Count; i++)
            {
                T Element = set[i];
                T Temp = itemsList.FirstOrDefault(x => x.IsEqual(Element));
                if (Temp == null)
                    return false;
            }
            return true;
        }
        public void Print()
        {
            Console.WriteLine(InString);
        }
    }
}
