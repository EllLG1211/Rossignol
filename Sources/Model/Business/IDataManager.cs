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

        bool checkUserExists(string? mail);

        void CreateEntryToConnectedUser(AbstractUser user, Entry entry);
        
        void RemoveEntry(AbstractUser user, Entry entry);

        void ShareEntryWith(ProprietaryEntry entry, string password);

        void UnshareEntryTo(ProprietaryEntry entry, string password);

        IEnumerable<Entry> GetEntries(AbstractUser user);
        IEnumerable<SharedEntry> GetSharedEntries(ConnectedUser user);

        /// <summary>
        /// Clear data of the dataManager;
        /// </summary>
        void clear();

        void save();

    }
}
