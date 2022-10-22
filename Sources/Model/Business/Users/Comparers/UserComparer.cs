using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Users.Comparers
{
    /// <summary>
    /// Equality comparer for the AbstractUser class and subclasses.
    /// Defined as an external class because AbstractUser is derived and that causes problem in equality protocol.
    /// <see cref="Entry.Equals(Entry?)"/> and <see cref="Entry.GetHashCode" call these methods/>
    /// </summary>
    public class UserComparer : IEqualityComparer<AbstractUser>
    {
        public bool Equals(AbstractUser? x, AbstractUser? y)
        {
            if (x == null || y == null) return x == null && y == null; //must return true if both members are null
            if (x.GetType() != y.GetType()) return false;
            return x.Uid.Equals(y.Uid) && x.Password.Equals(y.Password);
        }

        public int GetHashCode([DisallowNull] AbstractUser obj)
            => obj.Uid.GetHashCode() * 17
             + obj.Password.GetHashCode() * 17 ^ 2
             + obj.GetType().GetHashCode();
    }
}