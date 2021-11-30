using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class MyItem : IItem, ICloneable<MyItem>
    {
        public string Id { get; set; }

        public string InString { get { return Id; } }

        public MyItem(string Id) { this.Id = Id; }
        public bool IsEqual(IItem item)
        {
            if (item == null)
                return false;
            if (!(item is MyItem))
                return false;
            return (((MyItem)item).Id == Id);
        }

        public MyItem Clone()
        {
            return new MyItem(String.Copy(Id));
        }
        public bool IsEqual(MyItem other)
        {
            if (other == null) return false;
            if (other.Id != Id) return false;
            return true;
        }

    }
}
