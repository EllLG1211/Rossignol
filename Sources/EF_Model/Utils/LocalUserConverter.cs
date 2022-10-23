using EF_Model.Entities;
using EncryptedModel.Business.Users;

namespace EF_Model.Utils
{
    public static class LocalUserConverter
    {
        public static EncryptedLocalUser LToModel(this LocalUserEntity entity)
            =>  new EncryptedLocalUser(entity.EncryptionType, entity.Uid.ToString(), entity.Password, ProprietaryEntryConverter.ToModels(entity.OwnedEntries).ToList());

        public static IEnumerable<EncryptedLocalUser> LToModels(this IEnumerable<LocalUserEntity> entities)
        => entities.Select(e => e.LToModel());

        public static LocalUserEntity LToEntity(this EncryptedLocalUser model)
        =>  new LocalUserEntity
        {
                EncryptionType = model.EncryptionType,
                Uid = model.Uid,
                Password = model.EncryptedPassword,
                //Entries = ProprietaryEntryConverter.ToEntities(model.ownedEncryptedEntries)
            };

        public static IEnumerable<LocalUserEntity> LToEntities(this IEnumerable<EncryptedLocalUser> users)
        => users.Select(m => m.LToEntity());
    }
}
