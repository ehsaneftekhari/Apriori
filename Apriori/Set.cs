using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class Set<T> : IEnumerable, IEnumerator, ICloneable<Set<T>> where T : IItem, ICloneable<T>
    {
        private List<T> ElementsList;
        int position = -1;
        public IEnumerable<T> Elements
        {
            get
            {
                List<T> temp = new List<T>();
                foreach (T element in ElementsList) temp.Add(element.Clone());
                return temp;
            }
        }
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

        public Set()
        {
            ElementsList = new List<T>();
        }
        public Set(T Element) : this()
        {
            Add(Element);
        }
        public Set(IEnumerable<T> Elements) : this()
        {
            AddRange(Elements);
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

        //ICloneable<T>
        public Set<T> Clone()
        {
            return new Set<T>(ElementsList);
        }
        public bool IsEqual(Set<T> other)
        {
            if (other.Count != Count)
                return false;

            foreach (T Element in other)
                if (ElementsList.FirstOrDefault(x => x.IsEqual(Element)) == null)
                    return false;

            return true;
        }
        //ICloneable<T> End

        public void AddRange(IEnumerable<T> Elements)
        {
            foreach (T Element in Elements)
            {
                Add(Element);
            }
        }
        public bool Add(T Element)
        {
            if (ElementsList.FirstOrDefault<T>(x => x.IsEqual(Element)) == null)
            {
                ElementsList.Add(Element.Clone());
                return true;
            }
            return false;
        }
        public static Set<T> Merge(Set<T> set1, Set<T> set2)
        {
            Set<T> merged = set1.Clone();
            merged.AddRange(set2.Elements);
            return merged;
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
        public IEnumerable<Set<T>> SubSets()
        {
            List<Set<T>> List = new List<Set<T>>();
            for (int i = 0; i < Math.Pow(ElementsList.Count, 2); i++)
            {
                List.Add(GenerateSubset(Binary_Subset_Generator(i)));
            }
            return List;
        }
        private bool[] Binary_Subset_Generator(int subset_number)
        {
            bool[] Map = new bool[ElementsList.Count];
            for (int i = 0; subset_number > 0; i++)
            {
                try
                {
                    if (subset_number % 2 == 1)
                        Map[i] = true;
                    else
                        Map[i] = false;

                    subset_number /= 2;
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw ex;
                }
            }
            return Map;
        }
        private Set<T> GenerateSubset(bool[] Map)
        {
            Set<T> subsetSet = new Set<T>();
            for (int i = 0; i < Map.Length; i++)
            {
                if (Map[i])
                    subsetSet.Add(ElementsList.ElementAt(i));
            }
            return subsetSet;
        }
        public void SubtractionBy(Set<T> Secound)
        {
            foreach (T element in Secound)
                ((List<T>)Elements).RemoveAll(x => x.IsEqual(element));
        }
        public Set<T> Subtraction(Set<T> Secound)
        {
            return Subtraction(this, Secound);
        }
        public Set<T> Subtraction(Set<T> first, Set<T> Secound)
        {
            Set<T> ans = first.Clone();

            if (Secound == null) return ans;

            foreach (T element in Secound)
                ((List<T>)ans.Elements).RemoveAll(x => x.IsEqual(element));
            return ans;
        }
        public void Print()
        {
            Console.WriteLine(InString);
        }

    }
}
