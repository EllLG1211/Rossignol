using EF_Model.Entities;
using EncryptedModel.Business.Users;
using Model.Business.Users;
using EF_Model.Utils;

namespace EF_Model.Utils
{
    public static class ConnectedUserConverter
    {
        public static ConnectedUser ToModel(this ConnectedUserEntity entity)
            =>  new ConnectedUser(new Guid(entity.Uid), entity.Mail, entity.Password, EF_Model.Utils.EntryConverter.ToModels(entity.OwnedEntries.ToList()).ToList());

        public static IEnumerable<ConnectedUser> ToModels(this IEnumerable<ConnectedUserEntity> entities)
        => entities.Select(e => e.ToModel());

        public static ConnectedUserEntity ToEntity(this ConnectedUser user)
        {
            var userEntity = new ConnectedUserEntity
            {
                Uid = user.Uid.ToString(),
                Password = user.Password,
                Mail = user.Mail,
            };

            userEntity.SharedWith = user.SharedEntries.ToEntities(userEntity).ToList() ?? new List<EntryEntity>();
            userEntity.OwnedEntries = user.Entries.ToEntities(userEntity).ToList() ?? new List<EntryEntity>();
            return userEntity;
        }
        
        public static IEnumerable<ConnectedUserEntity> ToEntities(this IEnumerable<ConnectedUser> users)
        => users.Select(m => m.ToEntity());
    }
}
