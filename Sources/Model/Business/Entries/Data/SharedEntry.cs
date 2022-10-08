using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries
{
    public class SharedEntry : Entry
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

        private List<MailedUser> _sharedWith = new List<MailedUser>();
        public IEnumerable<MailedUser> SharedWith => new ReadOnlyCollection<MailedUser>(_sharedWith);

        public SharedEntry(string login, string password, string app, string? note) 
            : base(login, password, app, note){}
        
        public SharedEntry(string login, string password, string app)
            : this(login, password, app, string.Empty) { }

        public IReadOnlyList<MailedUser> GetSharedWith()
        {
            return _sharedWith.AsReadOnly();
        }

        public void ShareToUser(MailedUser user)
        {
            _sharedWith.Add(user);
        }

        public void UnshareToUser(MailedUser user)
        {
            _sharedWith.Remove(user);
        }
    }
}
