using Model.Business;
using Model.Business.Entries;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Model.Managers
{
    public class DataManager : IDataManager
    {
        public static readonly string serverPass = "1456485645864864fdz";

        public void clear()
        {
            using (var context = new RossignolContextLocal())
            {

            }
        }

        public void CreateEntryToConnectedUser(AbstractUser user, Entry entry)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entry> GetEntries(AbstractUser user)
        {
            throw new NotImplementedException();
        }

        public AbstractUser GetUser(string? mail, string password)
        {
            throw new NotImplementedException();
        }

        public void Register(AbstractUser user)
        {
            throw new NotImplementedException();
        }

        public void RemoveEntry(AbstractUser user, Entry entry)
        {
            throw new NotImplementedException();
        }

        public void ShareEntryWith(ProprietaryEntry entry, MailedUser user)
        {
            throw new NotImplementedException();
        }

        public void UnshareEntryTo(ProprietaryEntry entry, MailedUser user)
        {
            throw new NotImplementedException();
        }
    }
}
