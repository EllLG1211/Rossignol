using Model.Business;
using Model.Business.Entries;
using Model.Business.Users;
using System.Linq;

namespace Data
{
    public class Stub : IDataManager
    {
        private readonly List<AbstractUser> _users = new List<AbstractUser>();
        //TODO: implement shared entries within the stub
        //TODO: use EF within the stub for persistence
        //TODO: complete the missing code in this interface
        public Stub()
        {
            AbstractUser user = new ConnectedUser("test@test.com", "1234");
            ProprietaryEntry entry = new ProprietaryEntry("test", "1234", "discord");
            user.AddEntry(entry);
            Entry entry2 = new ProprietaryEntry("essai", "1234", "facebook");
            user.AddEntry(entry2);
            this._users.Add(user);

            MailedUser user2 = new ConnectedUser("moi@lui.com", "1234");
            Entry entry3 = new ProprietaryEntry("lorem", "ipsum", "discord");
            user.AddEntry(entry3);
            entry.ShareToUser(user2);
            this._users.Add(user2);
        }

        public void clear()
        {
            _users.Clear();
        }

        public void CreateEntryToConnectedUser(AbstractUser user, Entry entry)
        {
            user.AddEntry(entry);
        }

        public IEnumerable<Entry> GetEntries(AbstractUser user)
        {
            return user.Entries;
        }

        public AbstractUser GetUser(string? mail, string password)
        {
            if (mail == null)
            {
                if (password.Equals(_users.First().Password))
                {
                    return _users.First();
                }
                else
                {
                    throw new Exception("Password is incorrect");
                }
            }

            MailedUser? user = _users.Find(user => ((MailedUser)user).Mail.Equals(mail, StringComparison.Ordinal)) as MailedUser;
            if (user == null) throw new Exception("Unknown user");
            if (password.Equals(user.Password)) return user;
            throw new Exception("Uncorrect password");
        }

        public void Register(AbstractUser user)
        {
            _users.Add(user);
        }

        public void RemoveEntry(AbstractUser user, Entry entry)
        {
            user.RemoveEntry(entry);
        }

        public void save()
        {
           throw new NotImplementedException("saving is not supported in this stub");
        }

        public void ShareEntryWith(ProprietaryEntry entry, MailedUser user)
        {
            user.AddEntry(entry);   //same as for a regualr entry
            //The code here would be useless.
            //The code here would be useless.
            //The code here would be useless.
        }

        public void UnshareEntryTo(ProprietaryEntry entry, MailedUser user)
        {
            user.RemoveEntry(entry);
        }
    }
}