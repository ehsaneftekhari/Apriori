using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class Rule<T> : ICloneable<Rule<T>> where T : IItem, ICloneable<T>
    {
        Set<T> Assumption { get; set; }
        Set<T> result { get; set; }

        public string InString
        {
            get
            {
                return Assumption.InString +" -> "+ result.InString;
            }
        }

        public Rule<T> Clone()
        {
            throw new NotImplementedException();
        }

        public bool IsEqual(Rule<T> other)
        {
            throw new NotImplementedException();
        }
    }
}
