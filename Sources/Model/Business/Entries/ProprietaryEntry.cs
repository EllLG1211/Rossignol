using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries
{
    public class ProprietaryEntry : Entry
    {

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

        public readonly List<MailedUser> sharedWith;

        public ProprietaryEntry(string login, string password, string app, string note) 
            : base(login, password, app, note)
        {
            sharedWith = new List<MailedUser>();
        }

        public ProprietaryEntry(string login, string password, string app)
            : this(login, password, app, string.Empty) { }

        public IReadOnlyList<MailedUser> GetSharedWith()
        {
            return sharedWith.AsReadOnly();
        }

        public void ShareToUser(MailedUser user)
        {
            sharedWith.Add(user);
        }

        public void UnshareToUser(MailedUser user)
        {
            sharedWith.Remove(user);
        }
    }
}
