using EF_Model;
using EF_Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EF_Online.Managers
{
    public class EFManager
    {

        public static async Task ConstructDatabase(List<ConnectedUserEntity> users, DbContextOptions<RossignolContextOnline> options = null)
        {
            using (var context = (options == null ? new RossignolContextOnline() : new RossignolContextOnline(options)))
            {
                context.Database.EnsureCreated();
                context.OnlinesUsers.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
