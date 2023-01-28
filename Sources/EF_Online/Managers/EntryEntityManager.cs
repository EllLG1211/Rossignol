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

        public static async Task addEntry(ProprietaryEntry e, LocalUserEntity lue, DbContextOptions<RossignolContextOnline> options)
        {
            using (var context = new RossignolContextOnline(options))
            {
                context.EntriesSet.Add(e.ToEntity(lue));
                context.SaveChanges();
            }
        }

        public static async Task removeEntry(ProprietaryEntry e, LocalUserEntity lue, DbContextOptions<RossignolContextOnline> options)
        {
            using (var context = new RossignolContextOnline(options))
            {
                context.EntriesSet.Remove(e.ToEntity(lue));
                context.SaveChanges();
            }
        }

        public static async Task clearDB(DbContextOptions<RossignolContextOnline> options)
        {
            using (var context = new RossignolContextOnline(options))
            {
                context.EntriesSet.RemoveRange(context.EntriesSet);
                context.SaveChanges();
            }
        }

    }
}
