using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class MyApriori<T> where T : IItem
    {
        List<Transaction<T>> transactions { get; }
        public MyApriori()
        {
            transactions = new List<Transaction<T>>();
        }
        public MyApriori(IEnumerable<Transaction<T>> transactions) : this()
        {
            this.transactions.AddRange(transactions);
        }
        public void AddTransaction(Transaction<T> transaction)
        {
            this.transactions.Add(transaction);
        }
    }
}
