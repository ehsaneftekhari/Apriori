using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class Rule<T> where T : IItem
    {
        Set<T> Assumption { get; set;}
        Set<T> result { get; set;}
    }
}
