using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries
{
    /// <summary>
    /// Entry in the receiver password side
    /// </summary>
    public class SharedEntry : Entry
    {
        private ReadOnlyUser _owner;
        public ReadOnlyUser Owner => _owner;

        public SharedEntry(Guid uid, string login, string password, string app, string? note) 
            : base(uid, login, password, app, note)
        {   //TODO: uncomment and fix (yorick)
            /*if(owner == null) throw new ArgumentNullException(nameof(owner));
            _owner = owner;*/
        }

        public SharedEntry(string login, string password, string app, string? note)
            : this(Guid.NewGuid(), login, password, app, note) { }

        public SharedEntry(ReadOnlyUser owner, string login, string password, string app)
            : this(login, password, app, string.Empty) { }
    }
}
