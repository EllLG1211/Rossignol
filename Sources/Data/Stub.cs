using Model.Business;
using Model.Business.Entries;
using Model.Business.Users;
using System.Linq;

namespace Data
{
    public class Stub : IDataManager
    {
        private readonly List<AbstractUser> _users = new List<AbstractUser>();
        //TODO: use EF within the stub for persistence
        public Stub()
        {
            ConnectedUser user = new ConnectedUser("test@test.com", "1234");
            ProprietaryEntry entry = new ProprietaryEntry(user.Mail,"test", "1234", "discord");
            user.AddEntry(entry);
            Entry entry2 = new ProprietaryEntry(user.Mail,"essai", "1234", "facebook");
            user.AddEntry(entry2);
            this._users.Add(user);

            ConnectedUser user2 = new ConnectedUser("moi@lui.com", "1234");
            Entry entry3 = new ProprietaryEntry(user2.Mail,"lorem", "ipsum", "discord");
            user.AddEntry(entry3);
            entry.ShareToUser(user2);
            this._users.Add(user2);

            ConnectedUser user3 = new ConnectedUser("moi@them.com", "1234");
            this._users.Add(user3);
        }

        public bool checkUserExists(string? mail)
        {
            if (mail == null)
            {
                throw new ArgumentNullException("mail argument was null");
            }

            ConnectedUser? user = getUserFromMail(mail);
            return user != null;
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

        public IEnumerable<SharedEntry> GetSharedEntries(ConnectedUser user)
        {
            return user.SharedEntries;
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

            ConnectedUser? user = getUserFromMail(mail);
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

        public void ShareEntryWith(ProprietaryEntry entry, string mail)
        {
            ConnectedUser? mUser = getUserFromMail(mail);
            entry.ShareToUser(mUser);
            mUser.AddShared(entry.ShareToUser(mUser));
        }

        public void UnshareEntryTo(ProprietaryEntry entry, string mail)
        {
            ConnectedUser? mUser = getUserFromMail(mail);
            mUser.RemoveEntry(entry);
            getUserFromMail(entry.OwnerMail).ApplyUnshareToOriginalEntry(entry.Uid, mUser);
        }

        private ConnectedUser? getUserFromMail(string mail) => _users.Find(Luser => ((ConnectedUser)Luser).Mail.Equals(mail, StringComparison.Ordinal)) as ConnectedUser;
    }
}