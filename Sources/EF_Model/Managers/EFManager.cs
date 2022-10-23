using EF_Model.Entities;

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
