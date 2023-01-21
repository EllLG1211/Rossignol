using EF_Model;
using EF_Model.Managers;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data
{
    //TODO implement everything
    public class EFDataManager : IDataManager
    {
        SqliteConnection connexion = new("DataSource=:memory:");
        DbContextOptions<RossignolContextLocal> options;
        EFManager efm;
        public EFDataManager()
        {
            options = new DbContextOptionsBuilder<RossignolContextLocal>().UseSqlite(connexion).Options;
            efm = new EFManager();
        }
        public bool checkUserExists(string? mail)
        {
            throw new NotImplementedException();
        }

        public void clear()
        {
            throw new NotImplementedException();
        }

        public void CreateEntryToConnectedUser(AbstractUser user, Entry entry)
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

        public void Register(AbstractUser user)
        {
            throw new NotImplementedException();
        }

        public void RemoveEntry(AbstractUser user, Entry entry)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public void ShareEntryWith(ProprietaryEntry entry, string password)
        {
            throw new NotImplementedException();
        }

        public void UnshareEntryTo(ProprietaryEntry entry, string password)
        {
            throw new NotImplementedException();
        }
    }
}
