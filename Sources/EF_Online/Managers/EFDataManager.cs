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
        /// <summary>
        /// This is the main constructor of the EFDataManager
        /// </summary>
        /// <param name="dataSource">the data source of the EF database, defaults to memory for testing only!</param>
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

        public bool AddEntryToUser(AbstractUser user, Entry entry)
        {
            using (var context = new RossignolContextOnline(options))
            {
                ConnectedUserEntity usr = context.OnlinesUsers.First(u => u.Uid == user.Uid.ToString());
                var e = entry.ToEntity(usr);
                if (e == null) return false;

                usr.OwnedEntries.Add(e);

                context.OnlinesUsers.Update(usr);
                context.SaveChanges();
                return true;
            }
        }

        public IEnumerable<Entry> GetEntries(AbstractUser user)
        {
            using (var context = new RossignolContextOnline(options))
            {
                return context.OnlinesUsers.First(u => u.Uid == user.Uid.ToString()).OwnedEntries.ToModels();
            }
        }

        public IEnumerable<SharedEntry> GetSharedEntries(ConnectedUser user)
        {
            using (var context = new RossignolContextOnline(options))
            {
                return context.OnlinesUsers.First(u => u.Uid == user.Uid.ToString()).SharedWith.ToModelShareds();
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
                context.SaveChanges();
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

        public bool UpdateUser(AbstractUser user)
        {
            if(user is ConnectedUser) {
                ConnectedUser usr = (ConnectedUser)user;
                if (!checkUserExists(usr.Mail)) return false;
                using (var context = new RossignolContextOnline(options))
                {
                    context.OnlinesUsers.Update(usr.ToEntity());
                    context.SaveChanges();
                }
                return true;
            }
            else
            {
                return false; //unapplicable to this implementation
            }
        }

        public bool DeleteUser(AbstractUser user)
        {
            if (user is ConnectedUser)
            {
                ConnectedUser usr = (ConnectedUser)user;
                if (!checkUserExists(usr.Mail)) return false;
                using (var context = new RossignolContextOnline(options))
                {
                    context.OnlinesUsers.Remove(usr.ToEntity());
                    context.SaveChanges();
                }
                return true;
            }
            else
            {
                return false; //unapplicable to this implementation
            }
        }
    }
}
