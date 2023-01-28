using EF_Model;
using EF_Model.Entities;

namespace EF_Local.Managers
{
    public class EFManager
    {

        public async Task ConstructDatabase(List<EntryEntity> entities, List<ConnectedUserEntity> users)
        {
            using (var context = new RossignolContextOnline())
            {
                context.EntriesSet.AddRange(entities);
                context.OnlinesUsers.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
