using EF_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Model.Managers
{
    public class EFManager
    {

        public async Task ConstructDatabase(List<EntryEntity> entities, List<LocalUserEntity> users)
        {
            using (var context = new RossignolContext())
            {

                context.EncryptedEntriesSet.AddRange(entities);
                context.LocalUserSet.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
