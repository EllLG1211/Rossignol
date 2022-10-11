using EF_Model.Entities;
using Model.Business.Users;

namespace EF_Model.Utils
{
    public static class LocalUserExtension
    {
        public static LocalUser ToModel(this LocalUserEntity entity)
            => new LocalUser(entity.Password);
    }
}
