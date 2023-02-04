using EF_Model.Entities;
using EncryptedModel.Business.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EF_Local.Managers
{
    public class UserEntityManager
    {
        public static async Task addUser(LocalUserEntity lue, DbContextOptions<RossignolContextLocal> options = null)
        {
            using (var context = (options == null) ? new RossignolContextLocal() : new RossignolContextLocal(options))
            {
                context.LocalUsers.Add(lue);
                context.SaveChanges();
            }
        }

        public static async Task removeUser(LocalUserEntity cue, DbContextOptions<RossignolContextLocal> options = null)
        {
            using (var context = (options == null) ? new RossignolContextLocal() : new RossignolContextLocal(options))
            {
                context.LocalUsers.Remove(cue);
                context.SaveChanges();
            }
        }

        public static async Task updateUser(LocalUserEntity cue, DbContextOptions<RossignolContextLocal> options = null)
        {
            using (var context = (options == null) ? new RossignolContextLocal() : new RossignolContextLocal(options))
            {
                context.LocalUsers.Update(cue);
                context.SaveChanges();
            }
        }

        public static async Task removeUser(Guid uid, DbContextOptions<RossignolContextLocal> options = null)
        {
            using (var context = (options == null) ? new RossignolContextLocal() : new RossignolContextLocal(options))
            {
                context.LocalUsers.Remove(context.LocalUsers.First(s => s.Uid == uid.ToString()));
                context.SaveChanges();
            }
        }

        public static int returnUserCount(DbContextOptions<RossignolContextLocal> options = null)
        {
            using (var context = (options == null) ? new RossignolContextLocal() : new RossignolContextLocal(options))
            {
                return context.LocalUsers.Count();
            }
        }

        public static async Task RAZ(DbContextOptions<RossignolContextLocal> options = null)
        {
            using (var context = (options == null) ? new RossignolContextLocal() : new RossignolContextLocal(options))
            {
                context.Database.ExecuteSqlRaw("delete from LocalUsers");
                context.SaveChanges();
            }
        }
    }
}
