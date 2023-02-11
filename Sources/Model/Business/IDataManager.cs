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
        bool Register(AbstractUser user, string Mail);

        AbstractUser GetUser(string mail, string password = "");

        bool UpdateUser(AbstractUser user);

        bool checkUserExists(string? mail);

        bool AddEntryToUser(AbstractUser user, Entry entry);

        bool RemoveEntry(AbstractUser user, Entry entry);

        bool ShareEntryWith(ProprietaryEntry entry, string Mail, string password);

        bool UnshareEntryTo(ProprietaryEntry entry, string Mail);

        IEnumerable<Entry> GetEntries(AbstractUser user);
        IEnumerable<SharedEntry> GetSharedEntries(ConnectedUser user);

        bool DeleteUser(AbstractUser user);

        /// <summary>
        /// Clear data of the dataManager;
        /// </summary>
        void clear();

        /// <deprecated>
        ///     this isn't useful in this context...
        /// </deprecated>
        void save();

    }
}
