using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class MyApriori<T> where T : IItem, ICloneable<T>
    {
        List<Transaction<T>> transactions { get; }
        Set<T> items { get; set; }
        List<List<Set<T>>> FrequentSets { get; }
        List<Set<T>> InFrequentSets { get; }
        List<Rule<T>> Rules { get; }
        public double MinSupport { get; set; }
        public double MinConfident { get; set; }

        public MyApriori()
        {
            items = new Set<T>();
            transactions = new List<Transaction<T>>();
            FrequentSets = new List<List<Set<T>>>();
            InFrequentSets = new List<Set<T>>();
        }
        public MyApriori(IEnumerable<Transaction<T>> transactions) : this()
        {
            this.transactions.AddRange(transactions);
            foreach (Transaction<T> transaction in transactions)
                AddItems(transaction.Items);
        }

        public void AddItems(IEnumerable<T> items)
        {
            this.items.AddRange(items);
        }

        public bool AddItem(T item)
        {
            return items.Add(item);
        }

        public void AddTransaction(Transaction<T> transaction)
        {
            transactions.Add(transaction);
            AddItems(transaction.Items);
        }

        private double GetSupportOf(Set<T> set)
        {
            double count = 0;
            foreach (Transaction<T> transaction in transactions)
                if (transaction.Contains(set))
                    count++;
            return count / transactions.Count;
        }

        private bool CheckSupport(Set<T> set)
        {
            for(int i = 0; i < InFrequentSets.Count; i++)
            {
                Set<T> InFrequentSet = InFrequentSets[i];
                if (set.HasSubset(InFrequentSet))
                {
                    return false;
                }
            }
            double support = GetSupportOf(set);
            return support >= MinSupport;
        }

        private double GetConfidentOf(Rule<T> rule)
        {
            double count1 = 0;
            double count2 = 0;
            foreach (Transaction<T> transaction in transactions)
                if (transaction.Contains(rule.Assumption))
                {
                    count1++;
                    if (transaction.Contains(rule.Result))
                        count2++;
                }

            return count2 / count1;
        }

        private bool CheckConfident(Rule<T> rule)
        {
            double confident = GetConfidentOf(rule);
            return confident >= MinConfident;
        }

        public IEnumerable<Rule<T>> Process()
        {
            int count = 0;
            FrequentSets.Add(new List<Set<T>>());
            foreach (T item in items)
            {
                Set<T> ChallengedSet = new Set<T>(item);
                if (CheckSupport(ChallengedSet))
                {
                    FrequentSets.ElementAt(0).Add(ChallengedSet);
                    count++;
                }
                else
                    InFrequentSets.Add(ChallengedSet);
            }
            ///
            for (int i = 2; count >= 1; i++)
            {
                count = 0;
                FrequentSets.Add(new List<Set<T>>());
                for (int j = 0; j < FrequentSets.ElementAt(0).Count; j++)
                {
                    Set<T> SengleSet = FrequentSets.ElementAt(0).ElementAt(j);
                    for (int k = 0; k < FrequentSets.ElementAt(i - 2).Count; k++)
                    {
                        Set<T> item = FrequentSets.ElementAt(i - 2).ElementAt(k);

                        Set<T> ChallengedSet = Set<T>.Merge(item, SengleSet);
                        if (ChallengedSet.Count == i && (FrequentSets.ElementAt(i - 1).FirstOrDefault(x => x.HasSubset(ChallengedSet)) == null))
                            if (CheckSupport(ChallengedSet))
                            {
                                FrequentSets.ElementAt(i - 1).Add(ChallengedSet);
                                count++;
                            }
                            else
                                InFrequentSets.Add(ChallengedSet);
                    }
                }
            }
            ///


            return null;
        }
    }
}