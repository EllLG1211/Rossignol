using EF_Model.Entities;
using EncryptedModel.Business.Users;

namespace EF_Model.Managers
{
    public class UserEntityManager
    {
        public static async Task addUser(EncryptedLocalUser e, LocalUserEntity lue)
        {
            using (var context = new RossignolContextLocal())
            {
                //context.EncryptedEntriesSet.Add(e.ToEntity(lue));
                context.SaveChanges();
            }
        }
    }
}
