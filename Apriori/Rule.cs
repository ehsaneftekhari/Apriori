using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    class Rule<T> : ICloneable<Rule<T>> where T : IItem, ICloneable<T>
    {
        public Set<T> Assumption { get; set; }
        public Set<T> Result { get; set; }

        public string InString
        {
            get
            {
                return Assumption.InString +" -> "+ Result.InString;
            }
        }

        public Rule(Set<T> Assumption, Set<T> result)
        {
            this.Assumption = Assumption.Clone();
            this.Result = result.Clone();
        }

        public Rule<T> Clone()
        {
            return new Rule<T>(Assumption, Result);
        }

        public bool IsEqual(Rule<T> other)
        {
            if(other == null)
                return false;
            if(!other.Assumption.IsEqual(Assumption) || !Result.IsEqual(other.Result))
                return false;
            return true;
        }
    }
}
