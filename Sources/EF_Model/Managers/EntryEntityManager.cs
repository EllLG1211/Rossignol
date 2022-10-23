using EF_Model.Entities;
using EF_Model.Utils;
using EncryptedModel.Business.Entries;
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

        public static async Task addEntry(EncryptedProprietaryEntry e, LocalUserEntity lue, DbContextOptions<RossignolContextLocal> options)
        {
            using (var context = new RossignolContextLocal(options))
            {
                context.EncryptedEntriesSet.Add(e.ToEntity(lue));
                context.SaveChanges();
            }
        }

        public static async Task removeEntry(EncryptedProprietaryEntry e, LocalUserEntity lue, DbContextOptions<RossignolContextLocal> options)
        {
            using (var context = new RossignolContextLocal(options))
            {
                context.EncryptedEntriesSet.Remove(e.ToEntity(lue));
                context.SaveChanges();
            }
        }

        public static async Task clearDB(DbContextOptions<RossignolContextLocal> options)
        {
            using (var context = new RossignolContextLocal(options))
            {
                context.EncryptedEntriesSet.RemoveRange(context.EncryptedEntriesSet);
                context.SaveChanges();
            }
        }

    }
}
