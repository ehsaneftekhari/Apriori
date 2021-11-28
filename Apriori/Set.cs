using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class Set<T> where T : IItem
    {
        private List<T> ElementsList;
        public T this[int i]
        {
            get { return ElementsList.ElementAt(i); }
        }
        public string InString
        {
            get
            {
                string str = "{";
                foreach (T t in ElementsList)
                {
                    str += t.InString+", ";
                }
                str += "}";
                return str;
            }
        }

        public Set()
        {
            ElementsList = new List<T>();
        }
        public Set(T Item) : this()
        {
            ElementsList.Add(Item);
        }
        public Set(IEnumerable<T> Items) : this()
        {
            foreach (T Item in Items)
            {
                if (!ElementsList.Contains(Item))
                    ElementsList.Add(Item);
            }
        }
        public void AddItem(T Item)
        {
            if (!ElementsList.Contains(Item))
                ElementsList.Add(Item);
        }

        public void Print()
        {
            Console.WriteLine(InString);
        }
    }
}
