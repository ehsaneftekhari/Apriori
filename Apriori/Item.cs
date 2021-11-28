using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    interface IItem
    {
        string InString { get; }
        bool IsEqual(IItem item);
        void print();
    }
}
