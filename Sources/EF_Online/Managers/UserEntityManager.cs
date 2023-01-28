using EF_Model.Entities;
using EncryptedModel.Business.Users;

namespace EF_Model.Managers
{
    public class UserEntityManager
    {
        public static async Task addUser(ConnectedUserEntity cue)
        {
            using (var context = new RossignolContextOnline())
            {
                context.OnlinesUsers.Add(cue);
                context.SaveChanges();
            }
        }

        public static async Task removeUser(ConnectedUserEntity cue)
        {
            using (var context = new RossignolContextOnline())
            {
                context.OnlinesUsers.Remove(cue);
                context.SaveChanges();
            }
        }

        public static async Task removeUser(string mail)
        {
            using (var context = new RossignolContextOnline())
            {
                context.OnlinesUsers.First(s => s.Mail == mail);
                context.SaveChanges();
            }
        }

        public static async Task removeUser(Guid uid)
        {
            using (var context = new RossignolContextOnline())
            {
                context.OnlinesUsers.First(s => s.Uid == uid.ToString());
                context.SaveChanges();
            }
        }
    }
}
