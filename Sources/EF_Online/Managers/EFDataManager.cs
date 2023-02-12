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
        private string source;
        /// <summary>
        /// This is the main constructor of the EFDataManager
        /// </summary>
        /// <param name="dataSource">the data source of the EF database, defaults to memory for testing only!</param>
        public EFDataManager(string dataSource = ":memory:")
        {
            connection = new SqliteConnection($"DataSource={dataSource}");
            source = dataSource;
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
            using (var context = (options == null) ? new RossignolContextOnline() : new RossignolContextOnline(options))
            {
                context.OnlinesUsers.RemoveRange(context.OnlinesUsers.Include(u => u.OwnedEntries).Include(u => u.SharedWith));
                context.EntriesSet.RemoveRange(context.EntriesSet);
                context.ReferencedUsers.RemoveRange(context.ReferencedUsers);
                context.SaveChanges();
            }
        }

        public bool AddEntryToUser(AbstractUser user, Entry entry)
        {
            try
            {
                using (var context = new RossignolContextOnline(options))
                {
                    ConnectedUserEntity usr = context.OnlinesUsers.First(u => u.Uid == user.Uid.ToString());
                    var e = entry.ToEntity(usr);
                    if (e == null) return false;

                    //usr.OwnedEntries.Add(e);  //incorrect
                    e.Owner = usr;

                    context.EntriesSet.Add(e);

                    context.OnlinesUsers.Update(usr);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (System.InvalidOperationException se) //nothing
            {
                return false;
            }
        }

        public IEnumerable<Entry> GetEntries(AbstractUser user)
        {
            using (var context = new RossignolContextOnline(options))
            {
                ConnectedUserEntity k = context.OnlinesUsers.Include(u => u.OwnedEntries).First(usr => usr.Uid == user.Uid.ToString());
                return k.OwnedEntries.ToModels();
                //return context.EntriesSet.Where(entry => entry.Owner.Uid == user.Uid.ToString()).ToList().ToModels();
                //return context.OnlinesUsers.First(u => u.Uid == user.Uid.ToString()).OwnedEntries.ToModels();
            }
        }

        public IEnumerable<SharedEntry> GetSharedEntries(ConnectedUser user)
        {
            using (var context = new RossignolContextOnline(options))
            {
                try
                {
                    ConnectedUserEntity cu = context.OnlinesUsers.Include(u => u.SharedWith).First(u => u.Uid == user.Uid.ToString());
                    if (cu == null || cu.SharedWith == null)
                        return new List<EntryEntity>().ToModelShareds();
                    return cu.SharedWith.ToModelShareds();
                }catch(System.InvalidOperationException se) //nothing
                {
                    return new List<SharedEntry>();
                }
            }
        }

        public AbstractUser GetUser(string mail, string password = "")
        {
            using (var context = new RossignolContextOnline(options))
            {
                return context.OnlinesUsers.First(u => u.Mail == mail).ToModel();
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

        public bool ShareEntryWith(ProprietaryEntry entry,string Mail)
        {
            try
            {
                using (var context = new RossignolContextOnline(options))
                {
                    ConnectedUserEntity usr = context.OnlinesUsers.Include(u => u.OwnedEntries).First(u => u.Mail == entry.OwnerMail);
                    ConnectedUserEntity usrToShareTo = context.OnlinesUsers.Include(u => u.SharedWith).First(u => u.Mail == Mail);

                    if (usr != null && usrToShareTo != null)
                    {
                        //usr.OwnedEntries.First(f => f.Uid == entry.Uid.ToString()).SharedWith.Add(usrToShareTo);
                        usrToShareTo.SharedWith.Add(usr.OwnedEntries.First(f => f.Uid == entry.Uid.ToString()));

                        //context.OnlinesUsers.Update(usr);
                        context.OnlinesUsers.Update(usrToShareTo);
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (System.InvalidOperationException se) //nothing
            {
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
                    ConnectedUserEntity usr = context.OnlinesUsers.Include(u => u.SharedWith).First(u => u.Uid == user.Uid.ToString());
                    usr.SharedWith.Remove(usr.SharedWith.First(e => e.Uid == entry.Uid.ToString()));
                    context.OnlinesUsers.Update(usr);
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
                ConnectedUserEntity usr = ((ConnectedUser)user).ToEntity();
                if (!checkUserExists(usr.Mail)) return false;
                using (var context = new RossignolContextOnline(options))
                {
                    context.OnlinesUsers.Where(uxr=> uxr.Uid == usr.Uid).ExecuteDelete();
                    context.SaveChanges();
                }
                return true;
            }
            else
            {
                return false; //unapplicable to this implementation
            }
        }

        public bool CreateEntryToConnectedUser(AbstractUser user, Entry entry)
        {
            return AddEntryToUser(user, entry);
        }

        private void Exterminatus()
        {
            try
            {
                connection.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(source);
            }
            catch (Exception ex) 
            { 
            }
        }
    }
}
