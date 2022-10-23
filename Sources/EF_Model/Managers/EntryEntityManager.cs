using EF_Model.Entities;
using EF_Model.Utils;
using EncryptedModel.Business.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Model.Managers
{
    public class EntryEntityManager
    {
        public async Task addEntry(EncryptedProprietaryEntry e, LocalUserEntity lue)
        {
            using (var context = new RossignolContextLocal())
            {
                context.EncryptedEntriesSet.Add(e.ToEntity(lue));
                context.SaveChanges();
            }
        }

        public async Task removeEntry(EncryptedProprietaryEntry e, LocalUserEntity lue)
        {
            using (var context = new RossignolContextLocal())
            {
                context.EncryptedEntriesSet.Remove(e.ToEntity(lue));
                context.SaveChanges();
            }
        }

        //public async Task<EncryptedProprietaryEntry> GetProprietaryEntries

    }
}
