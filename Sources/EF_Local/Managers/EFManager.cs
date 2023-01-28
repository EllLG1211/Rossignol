using EF_Model;
using EF_Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_Local.Managers
{
    public class EFManager
    {

        public async Task ConstructDatabase(List<LocalUserEntity> users, DbContextOptions<RossignolContextLocal> options = null)
        {
            using (var context = (options == null ? new RossignolContextLocal() : new RossignolContextLocal(options)))
            {
                context.LocalUsers.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
