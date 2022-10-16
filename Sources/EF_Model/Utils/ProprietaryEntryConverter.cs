using EF_Model.Entities;
using Model.Business.Entries.Serialized;

namespace EF_Model.Utils
{
    public static class ProprietaryEntryConverter
    {
        public static EncryptedSharedEntry ToModel(this EntryEntity entity)
            => new EncryptedSharedEntry(entity.EncryptionType, entity.Uid.ToString(), entity.Login, entity.Password, entity.App, entity.Note);

        public static IEnumerable<EncryptedSharedEntry> ToModels(this IEnumerable<EntryEntity> entities)
            => entities.Select(e => e.ToModel());

        public static EntryEntity ToEntity(this EncryptedSharedEntry model)
            => new EntryEntity
            {
                EncryptionType = model.encryptionType,
                Uid = model.Uid,
                Login = model.EncryptedLogin,
                Password = model.EncryptedPassword,
                App = model.EncryptedApp,
                Note = model.EncryptedNote,
            };

        public static IEnumerable<EntryEntity> ToEntities(this IEnumerable<EncryptedSharedEntry> models)
            => models.Select(m => m.ToEntity());
    }
}
