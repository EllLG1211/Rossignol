using Model.Business;
using Model.Business.Entries;
using Model.Business.Users;
using System.Linq;

namespace Data
{
    public class Stub : IDataManager
    {
        private readonly List<AbstractUser> _users = new List<AbstractUser>();

        public Stub()
        {
            AbstractUser user = new ConnectedUser("test@test.com","1234");
            ProprietaryEntry entry = new ProprietaryEntry("test", "1234", "discord");
            user.AddEntry(entry);
            Entry entry2 = new ProprietaryEntry("essai", "1234", "facebook");
            user.AddEntry(entry2);
            this._users.Add(user);

            MailedUser user2 = new ConnectedUser("moi@lui.com", "1234");
            Entry entry3 = new ProprietaryEntry("lorem", "ipsum", "discord");
            user.AddEntry(entry2);
            entry.ShareToUser(user2);
            this._users.Add(user2);
        }

        public void clear()
        {
            
        }

        public void CreateEntryToConnectedUser(AbstractUser user, Entry entry)
        {
            
        }

        public IEnumerable<Entry> GetEntries(AbstractUser user)
        {
            return user.Entries;
        }

        public AbstractUser? GetUser(string? mail, string password)
        {
            if(mail == null)
            {
                if (password.Equals(_users.First().Password))
                {
                    return _users.First();
                } else
                {
                    throw new Exception("Password is incorrect");
                }
            }

            MailedUser? user = _users.Find(user => ((MailedUser)user).Mail.Equals(mail, StringComparison.Ordinal)) as MailedUser;
            if (user == null) throw new Exception("Unknown user");
            if (password.Equals(user.Password)) return user;
            return null;
        }

        public void Register(AbstractUser user)
        {
            
        }

        public void RemoveEntry(AbstractUser user, Entry entry)
        {
            
        }

        public void ShareEntryWith(ProprietaryEntry entry, MailedUser user)
        {
        
        }

        public void UnshareEntryTo(ProprietaryEntry entry, MailedUser user)
        {
        
        }
    }
}