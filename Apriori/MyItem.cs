using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class MyItem : IItem
    {
        public string Id { get; set; }

        public string InString { get { return Id; } }

        public MyItem(string Id) { this.Id = Id; }
        public bool IsEqual(IItem item)
        {
            if (item == null)
                return false;
            if(!(item is MyItem))
                return false;
            return (((MyItem)item).Id == Id);
        }

        public void print()
        {
            Console.WriteLine(Id);
        }
    }
}
