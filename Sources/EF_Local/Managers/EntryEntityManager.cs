using EF_Model.Entities;
using EF_Model.Utils;
using Model.Business.Entries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Model.Managers
{
    public class EntryEntityManager
    {

        public static async Task addEntry(ProprietaryEntry e, LocalUserEntity lue, DbContextOptions<RossignolContextLocal> options)
        {
            using (var context = new RossignolContextLocal(options))
            {
                context.EntriesSet.Add(e.ToEntity(lue));
                context.SaveChanges();
            }
        }

        public static async Task removeEntry(ProprietaryEntry e, LocalUserEntity lue, DbContextOptions<RossignolContextLocal> options)
        {
            using (var context = new RossignolContextLocal(options))
            {
                context.EntriesSet.Remove(e.ToEntity(lue));
                context.SaveChanges();
            }
        }

        public static async Task updateEntry(ProprietaryEntry e, LocalUserEntity lue, DbContextOptions<RossignolContextLocal> options)
        {
            using (var context = new RossignolContextLocal(options))
            {
                context.EntriesSet.Update(e.ToEntity(lue));
                context.SaveChanges();
            }
        }

        public static async Task updateEntry(EntryEntity e, LocalUserEntity lue, DbContextOptions<RossignolContextLocal> options)
        {
            using (var context = new RossignolContextLocal(options))
            {
                context.EntriesSet.Update(e);
                context.SaveChanges();
            }
        }

        public static async Task removeEntry(EntryEntity e, LocalUserEntity lue, DbContextOptions<RossignolContextLocal> options)
        {
            using (var context = new RossignolContextLocal(options))
            {
                if(!context.LocalUsers.Contains(lue)) throw new ArgumentException(nameof(lue));
                if (context.EntriesSet.Contains(e))
                {
                    context.EntriesSet.Remove(e);
                    context.LocalUsers.First(s => s.Uid == lue.Uid).OwnedEntries.Remove(e);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException(nameof(e));
                }
            }
        }

        public static async Task clearDB(DbContextOptions<RossignolContextLocal> options)
        {
            using (var context = new RossignolContextLocal(options))
            {
                context.EntriesSet.RemoveRange(context.EntriesSet);
                context.SaveChanges();
            }
        }
        public static async Task RAZ(DbContextOptions<RossignolContextLocal> options = null)
        {
            using (var context = (options == null) ? new RossignolContextLocal() : new RossignolContextLocal(options))
            {
                context.Database.ExecuteSqlRaw("delete from EntriesSet");
                context.SaveChanges();
            }
        }

    }
}
