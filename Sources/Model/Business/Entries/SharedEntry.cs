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
        private string _ownerMail;
        public string OwnerMail => _ownerMail;

        public SharedEntry(Guid uid, string login, string password, string app, string? note) 
            : base(uid, login, password, app, note) {}

        public SharedEntry(string login, string password, string app, string? note)
            : this(Guid.NewGuid(), login, password, app, note) { }

        public SharedEntry(ReadOnlyUser owner, string login, string password, string app)
            : this(login, password, app, string.Empty) {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            _ownerMail = owner.Mail;
        }
    }
}