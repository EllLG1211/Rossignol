using EF_Model.Entities;
using Model.Business.Entries;

namespace EF_Model.Utils
{
    public static class SharedEntryConverter
    {
        public static SharedEntry SToModel(this EntryEntity entity)
            => new SharedEntry(new Guid(entity.Uid), entity.Login, entity.Password, entity.App, entity.Note);

        public static IEnumerable<SharedEntry> SToModels(this IEnumerable<EntryEntity> entities)
            => entities.Select(e => e.SToModel());

        public static EntryEntity ToEntity(this SharedEntry model, LocalUserEntity owner)
            => new EntryEntity
            {
                Uid = model.Uid.ToString(),
                Login = model.Login,
                Password = model.Password,
                App = model.App,
                Note = model.Note,
                Owner = owner
            };

        public static IEnumerable<EntryEntity> ToEntities(this IEnumerable<SharedEntry> models, LocalUserEntity owner)
            => models.Select(m => m.ToEntity(owner));
    }
}
