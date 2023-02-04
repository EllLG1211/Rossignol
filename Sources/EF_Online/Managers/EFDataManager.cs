using EF_Model.Entities;
using EF_Model.Utils;
using EF_Online;
using EF_Online.Managers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.Api.Entities;
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
    public class EFDataManager : IDataManager
    {
        SqliteConnection connection;
        DbContextOptions<RossignolContextOnline> options;
        public EFDataManager(string dataSource = ":memory:")
        {
            connection = new SqliteConnection($"DataSource={dataSource}");
            connection.Open();
            options = new DbContextOptionsBuilder<RossignolContextOnline>().UseSqlite(connection).Options;
        }

        public bool checkUserExists(string? mail)
        {
            using (var context = new RossignolContextOnline(options))
            {
                return context.OnlinesUsers.Any(u => u.Mail == mail);
            }
        }

        public void clear()
        {
            UserEntityManager.RAZ().RunSynchronously();
        }

        public bool CreateEntryToConnectedUser(AbstractUser user, Entry entry)
        {
            using (var context = new RossignolContextOnline(options))
            {
                var e = entry.ToEntity(context.OnlinesUsers.First(u => new Guid(u.Uid) == user.Uid));
                if (e != null)
                {
                    context.OnlinesUsers.First(u => new Guid(u.Uid) == user.Uid).OwnedEntries.Add(e);
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Entry> GetEntries(AbstractUser user)
        {
            using (var context = new RossignolContextOnline(options))
            {
                return context.OnlinesUsers.First(u => new Guid(u.Uid) == user.Uid).OwnedEntries.ToModels();
            }
        }

        public IEnumerable<SharedEntry> GetSharedEntries(ConnectedUser user)
        {
            using (var context = new RossignolContextOnline(options))
            {
                return context.OnlinesUsers.First(u => new Guid(u.Uid) == user.Uid).SharedWith.ToModelShareds();
            }
        }

        public AbstractUser GetUser(string? mail, string password)
        {
            using (var context = new RossignolContextOnline(options))
            {
                return context.OnlinesUsers.First(u => u.Mail == mail && u.Password == password).ToModel();
            }
        }

        public bool Register(AbstractUser user, string Mail)
        {
            ConnectedUser cu = new ConnectedUser(user.Uid, Mail, user.Password, user.Entries.ToList());
            using (var context = new RossignolContextOnline(options))
            {
                if (context.OnlinesUsers.Any(u => u.Mail == Mail)) return false;
                context.OnlinesUsers.Add(cu.ToEntity());
                return true;
            }
        }

        public bool RemoveEntry(AbstractUser user, Entry entry)
        {
            using (var context = new RossignolContextOnline(options))
            {
                if (context.EntriesSet.First(e => e.Uid == entry.Uid.ToString() && e.Owner.Uid == user.Uid.ToString()) != null)
                {
                    context.EntriesSet.Remove(context.EntriesSet.First(e => e.Uid == entry.Uid.ToString() && e.Owner.Uid == user.Uid.ToString()));
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public void save()
        {
            throw new NotImplementedException("no");
        }

        public bool ShareEntryWith(ProprietaryEntry entry,string Mail, string password)
        {
            using (var context = new RossignolContextOnline(options))
            {
                ConnectedUser user = context.OnlinesUsers.First(u=> u.Mail == Mail && u.Password == password).ToModel();
                if (user != null)
                {
                    context.OnlinesUsers.First(u => u.Uid == user.Uid.ToString()).SharedWith
                        .Add(new SharedEntry(new ReadOnlyUser(user),entry.Login, entry.Password,entry.App).ToEntity(user.ToEntity()));
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool UnshareEntryTo(ProprietaryEntry entry, string Mail)
        {
            using (var context = new RossignolContextOnline(options))
            {
                ConnectedUser user = context.OnlinesUsers.First(u => u.Mail == Mail).ToModel();
                if (user != null)
                {
                    context.OnlinesUsers.First(u => u.Uid == user.Uid.ToString()).SharedWith
                        .Remove(new SharedEntry(new ReadOnlyUser(user), entry.Login, entry.Password, entry.App).ToEntity(user.ToEntity()));
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
