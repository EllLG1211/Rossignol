using Model.Business.Entries;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business
{
    public interface IDataManager
    {
        void Register(AbstractUser user);

        AbstractUser GetUser(string? mail, string password);

        void CreateEntryToConnectedUser(AbstractUser user, Entry entry);
        
        void RemoveEntry(AbstractUser user, Entry entry);

        void ShareEntryWith(ProprietaryEntry entry, MailedUser user);

        void UnshareEntryTo(ProprietaryEntry entry, MailedUser user);

        IEnumerable<Entry> GetEntries(AbstractUser user);

        /// <summary>
        /// Clear data of the dataManager;
        /// </summary>
        void clear();

        void save();

    }
}
