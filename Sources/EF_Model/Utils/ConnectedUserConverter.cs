using EF_Model.Entities;
using EncryptedModel.Business.Users;

namespace EF_Model.Utils
{
    public static class ConnectedUserConverter
    {
        public static EncryptedConnectedUser ToModel(this ConnectedUserEntity entity)
            =>  new EncryptedConnectedUser(entity.EncryptionType, entity.Uid.ToString(), entity.Mail, entity.Password, ProprietaryEntryConverter.ToModels(entity.OwnedEntries).ToList());

        public static IEnumerable<EncryptedConnectedUser> ToModels(this IEnumerable<ConnectedUserEntity> entities)
        => entities.Select(e => e.ToModel());

        public static ConnectedUserEntity ToEntity(this EncryptedConnectedUser model) =>
         new ConnectedUserEntity
            {
                EncryptionType = model.EncryptionType,
                Uid = model.Uid,
                Password = model.EncryptedPassword,
                Mail = model.EncryptedMail,
                SharedWith = model.encryptedSharedWith.ToEntities(new ConnectedUserEntity()).ToList(),
                OwnedEntries = model.ownedEncryptedEntries.ToEntities(new ConnectedUserEntity()).ToList()
         };
        
        public static IEnumerable<ConnectedUserEntity> ToEntities(this IEnumerable<EncryptedConnectedUser> users)
        => users.Select(m => m.ToEntity());
    }
}
