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

        public Rule(Set<T> Assumption, Set<T> result)
        {
            this.Assumption = Assumption.Clone();
            this.result = result.Clone();
        }

        public Rule<T> Clone()
        {
            return new Rule<T>(Assumption, result);
        }

        public bool IsEqual(Rule<T> other)
        {
            if(other == null)
                return false;
            if(!other.Assumption.IsEqual(Assumption) || !result.IsEqual(other.result))
                return false;
            return true;
        }
    }
}
