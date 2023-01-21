using EF_Model.Entities;
using Model.Business.Entries;
using Model.Business.Users;

namespace EF_Model.Utils
{
    public static class EntryConverter
    {
        public static ProprietaryEntry ToModel(this EntryEntity entity)
        {
            ProprietaryEntry em = new ProprietaryEntry(//entity.EncryptionType,
                entity.Owner is ConnectedUserEntity ? ((ConnectedUserEntity)entity.Owner).Mail : "",
                new Guid(entity.Uid), 
                entity.Login, 
                entity.Password, 
                entity.App, 
                entity.Note);
            return em;
        }

        public static IEnumerable<Entry> ToModels(this IEnumerable<EntryEntity> entities)
            => entities.Select(e => e.ToModel());

        public static EntryEntity ToEntity(this Entry model, LocalUserEntity owner)
            => new EntryEntity
            {
                Uid = model.Uid.ToString(),
                Login = model.Login,
                Password = model.Password,
                App = model.App,
                Note = model.Note,
                Owner = owner
            };

        public static IEnumerable<EntryEntity> ToEntities(this IEnumerable<Entry> models, LocalUserEntity owner)
            => models.Select(m => m.ToEntity(owner));
    }
}
