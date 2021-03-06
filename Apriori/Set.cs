using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Apriori
{
    class Set<T> : IEnumerable, IEnumerator, ICloneable<Set<T>> where T : IItem, ICloneable<T>
    {
        private List<T> ElementsList;
        public int position = -1;
        public IEnumerable<T> Elements
        {
            get
            {
                List<T> temp = new List<T>();
                foreach (T element in ElementsList) temp.Add(element.Clone());
                return temp;
            }
        }
        public int Count
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
        public bool IsEmpty
        {
            get => ElementsList.Count == 0;
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
        public object Current
        {
            get { return this[position]; }
        }
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
            if (other == null)
                return false;

            if (other.Count != Count)
                return false;

            for (int i = 0; i < Count; i++)
            {
                T element = other[i];
                if (ElementsList.FirstOrDefault(x => x.IsEqual(element)) == null)
                    return false;
            }

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
        public void MergeBy(Set<T> set)
        {
            AddRange(set.Elements);
        }
        public Set<T> Merge(Set<T> set)
        {
            return Merge(this, set);
        }
        public static Set<T> Merge(Set<T> set1, Set<T> set2)
        {
            Set<T> merged = set1.Clone();
            merged.AddRange(set2.Elements);
            return merged;
        }
        public void SubtractionBy(Set<T> Secound)
        {
            foreach (T element in Secound)
                ElementsList.RemoveAll(x => x.IsEqual(element));
        }
        public Set<T> Subtraction(Set<T> Secound)
        {
            Set<T> ans = this.Clone();
            if (Secound == null) return ans;
            ans.SubtractionBy(Secound);
            return ans;
        }
        public static Set<T> Subtraction(Set<T> first, Set<T> Secound)
        {            
            return first.Subtraction(Secound);
        }
        public bool Contains(T Element)
        {
            return ElementsList.FirstOrDefault(x => x.IsEqual(Element)) != null;
        }
        public bool HasSubset(Set<T> subset)
        {
            if (subset == null)
                return true;

            for (int i = 0; i < subset.Count; i++)
            {
                T element = subset[i];
                if (!Contains(element))
                    return false;
            }

            return true;
        }
        public bool IsSubSetOf(Set<T> set)
        {
            if (set == null)
                if (Count == 0)
                    return true;
                else
                    return false;

            for (int i = 0; i < ElementsList.Count; i++)
            {
                T Element = ElementsList[i];
                if (!set.Contains(Element))
                    return false;
            }
            return true;
        }
        public IEnumerable<Set<T>> SubSets()
        {
            List<Set<T>> List = new List<Set<T>>();
            double lim = Math.Pow(2, ElementsList.Count);
            for (int i = 0; i < lim; i++)
            {
                List.Add(GenerateSubset(Binary_Subset_Generator(i)));
            }
            return List;
        }
        //public bool IsEqual(Set<T> set)
        //{
        //    if (set == null)
        //        return false;

        //    if (set.Count == Count)
        //        for (int i = 0; i < Count; i++)
        //        {
        //            T element = set[i];
        //            if (ElementsList.FirstOrDefault(x => x.IsEqual(element)) == null)
        //                return false;
        //        }
        //    else
        //        return false;

        //    return true;
        //}
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
        public void Print()
        {
            Console.WriteLine(InString);
        }
    }
}
