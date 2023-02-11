using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Model.Business;
using Model.Business.Entries;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Local.Managers
{
    public class EFLDataManager : IDataManager
    {
        SqliteConnection connection;
        DbContextOptions<RossignolContextLocal> options;
        public EFLDataManager(string dataSource = ":memory:")
        {
            connection = new SqliteConnection($"DataSource={dataSource}");
            connection.Open();
            options = new DbContextOptionsBuilder<RossignolContextLocal>().UseSqlite(connection).Options;
        }

        /// <summary>
        /// the local one uses the GUID!
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool checkUserExists(string? guid)
        {
            using (var context = new RossignolContextLocal(options))
            {
                return context.LocalUsers.Any(u => u.Uid == guid);
            }
        }

        public void clear()
        {
            throw new NotImplementedException();
        }

        public bool CreateEntryToConnectedUser(AbstractUser user, Entry entry)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entry> GetEntries(AbstractUser user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SharedEntry> GetSharedEntries(ConnectedUser user)
        {
            throw new NotImplementedException();
        }

        public AbstractUser GetUser(string? mail, string password)
        {
            throw new NotImplementedException();
        }

        public bool Register(AbstractUser user, string Mail)
        {
            throw new NotImplementedException();
        }

        public bool RemoveEntry(AbstractUser user, Entry entry)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public bool ShareEntryWith(ProprietaryEntry entry, string Mail, string password)
        {
            throw new NotImplementedException();
        }

        public bool UnshareEntryTo(ProprietaryEntry entry, string Mail)
        {
            throw new NotImplementedException();
        }
    }
}