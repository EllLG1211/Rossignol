using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries
{
    public class EntryComparer : IEqualityComparer<Entry>
    {
        public bool Equals(Entry? x, Entry? y)
        {
            if (x == null || y == null) return x == null && y == null; //must return true if both members are null
            if (x.GetType() != y.GetType()) return false;
            return x.Label.Equals(y.Label) && x.Password.Equals(y.Password);
        }

        public int GetHashCode([DisallowNull] Entry obj) 
            => obj.Label.GetHashCode() * 17 + obj.Password.GetHashCode() * 17^2 + obj.GetType().GetHashCode();
    }
}
