using EF_Model.Entities;
using EncryptedModel.Business.Users;

namespace EF_Model.Utils
{
    public static class LocalUserConverter
    {
        public static EncryptedLocalUser ToModel(this LocalUserEntity entity)
            => new EncryptedLocalUser(entity.EncryptionType, entity.Uid.ToString(), entity.Password, ProprietaryEntryConverter.ToModels(entity.Entries).ToList());


        public static LocalUserEntity ToEntity(this EncryptedLocalUser model)
        =>  new LocalUserEntity
            {
                EncryptionType = model.EncryptionType,
                Uid = model.Uid,
                Password = model.EncryptedPassword,
                //Entries = ProprietaryEntryConverter.ToEntities(model.ownedEncryptedEntries)
            };
    }
}
