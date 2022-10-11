using Model.Business.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Users
{
    /// <summary>
    /// User when he's not logged in.
    /// </summary>
    public class LocalUser: AbstractUser
    {
        public LocalUser(string password): base(password){}

        public LocalUser(string password, List<Entry> entries) : base(password, entries){}
    }
}
