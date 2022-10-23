using EF_Model.Entities;
using EncryptedModel.Business.Entries;

namespace EF_Model.Utils
{
    public static class SharedEntryConverter
    {
        public static EncryptedSharedEntry SToModel(this EntryEntity entity)
            => new EncryptedSharedEntry(entity.EncryptionType, entity.Uid.ToString(), entity.Login, entity.Password, entity.App, entity.Note);

        public static IEnumerable<EncryptedSharedEntry> SToModels(this IEnumerable<EntryEntity> entities)
            => entities.Select(e => e.SToModel());

        public static EntryEntity ToEntity(this EncryptedSharedEntry model, LocalUserEntity owner)
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

        public static IEnumerable<EntryEntity> ToEntities(this IEnumerable<EncryptedSharedEntry> models, LocalUserEntity owner)
            => models.Select(m => m.ToEntity(owner));
    }
}
