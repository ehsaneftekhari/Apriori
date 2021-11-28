using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class Set<T> : IEnumerable, IEnumerator where T : IItem
    {
        private List<T> ElementsList;
        int position = -1;
        int Count
        {
            get
            {
                return ElementsList.Count;
            }
        }
        public T this[int i]
        {
            get { return ElementsList.ElementAt(i); }
        }
        public string InString
        {
            get
            {
                string str = "{";
                for (int i = 0; i < ElementsList.Count; i++)
                {
                    str += ElementsList.ElementAt(i).InString;
                    if (i < ElementsList.Count - 1)
                        str += ", ";
                }
                str += "}";
                return str;
            }
        }

        //IEnumerator
        public object Current { get { return this[position]; } }
        public bool MoveNext()
        {
            position++;
            return (position < Count);
        }
        public void Reset()
        {
            position = -1;
        }
        //IEnumerator End

        //IEnumerable
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
        //IEnumerable End

        public Set()
        {
            ElementsList = new List<T>();
        }
        public Set(T Element) : this()
        {
            ElementsList.Add(Element);
        }
        public Set(IEnumerable<T> Elements) : this()
        {
            foreach (T Element in Elements)
            {
                if (!ElementsList.Contains(Element))
                    ElementsList.Add(Element);
            }
        }
        public void AddElement(T Element)
        {
            if (!ElementsList.Contains(Element))
                ElementsList.Add(Element);
        }
        public bool Contains(T Element)
        {
            return ElementsList.Contains(Element);
        }
        public bool HaveSubSet(Set<T> set)
        {
            if (set == null)
                return true;

            foreach (T Element in set)
                if (!Contains(Element))
                    return false;

            return true;
        }
        public bool IsSubSetOf(Set<T> set)
        {
            if (set == null)
                if (Count == 0)
                    return true;
                else
                    return false;

            foreach (T Element in ElementsList)
                if (!set.Contains(Element))
                    return false;


            return true;
        }
        public void Print()
        {
            Console.WriteLine(InString);
        }
    }
}
