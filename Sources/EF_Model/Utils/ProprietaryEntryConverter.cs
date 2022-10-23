using EF_Model.Entities;
using EncryptedModel.Business.Entries;
using EncryptedModel.Business.Users;

namespace EF_Model.Utils
{
    public static class ProprietaryEntryConverter
    {
        public static EncryptedProprietaryEntry ToModel(this EntryEntity entity)
        {
            EncryptedProprietaryEntry em = new EncryptedProprietaryEntry(entity.EncryptionType, 
                entity.Uid.ToString(), 
                entity.Login, 
                entity.Password, 
                entity.App, 
                entity.Note);
            return em;
        }

        public static IEnumerable<EncryptedProprietaryEntry> ToModels(this IEnumerable<EntryEntity> entities)
            => entities.Select(e => e.ToModel());

        public static EntryEntity ToEntity(this EncryptedProprietaryEntry model, LocalUserEntity owner)
            => new EntryEntity
            {
                EncryptionType = model.encryptionType,
                Uid = model.Uid,
                Login = model.EncryptedLogin,
                Password = model.EncryptedPassword,
                App = model.EncryptedApp,
                Note = model.EncryptedNote,
                Owner = owner
            };

        public static IEnumerable<EntryEntity> ToEntities(this IEnumerable<EncryptedProprietaryEntry> models, LocalUserEntity owner)
            => models.Select(m => m.ToEntity(owner));
    }
}
