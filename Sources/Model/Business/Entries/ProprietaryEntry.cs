using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries
{
    /// <summary>
    /// Entry owner side
    /// </summary>
    public class ProprietaryEntry : Entry
    {
        public string OwnerMail{get;protected set;}

        public new string Login 
        { 
            get => base.Login;
            set => base.Login = value; 
        }

        public new string App
        {
            get => base.App;
            set => base.App = value;
        }

        public new string? Note
        { 
            get => base.Note; 
            set => base.Note = value; 
        }

        private readonly List<MailedUser> _sharedWith = new List<MailedUser>();
        public IEnumerable<MailedUser> SharedWith => new ReadOnlyCollection<MailedUser>(_sharedWith);

        public ProprietaryEntry(string ownerMail, Guid uid, string login, string password, string app, string? note) 
            : base(uid, login, password, app, note){
            if (ownerMail == null)
                throw new ArgumentNullException(nameof(ownerMail));
            OwnerMail = ownerMail; 
        }

        public ProprietaryEntry(string ownerMail, string login, string password, string app, string? note) 
            : this(ownerMail, Guid.NewGuid(), login, password, app, note){
            if (ownerMail == null)
                throw new ArgumentNullException(nameof(ownerMail));
            OwnerMail = ownerMail;
        }

        public ProprietaryEntry(string ownerMail, string login, string password, string app)
            : this(ownerMail, Guid.NewGuid(), login, password, app, string.Empty) { 
            if(ownerMail == null)
                throw new ArgumentNullException(nameof(ownerMail));
            OwnerMail = ownerMail;
        }

        public SharedEntry ShareToUser(MailedUser user)
        {
            _sharedWith.Add(user);
            return new SharedEntry(new ReadOnlyUser(OwnerMail, ""), Login, Password, App);
        }

        public void UnshareToUser(MailedUser user)
        {
            _sharedWith.Remove(user);
        }
    }
}
