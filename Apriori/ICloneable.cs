using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apriori
{
    interface ICloneable<T>
    {
        T Clone ();
        bool IsEqual (T other);
    }
}
