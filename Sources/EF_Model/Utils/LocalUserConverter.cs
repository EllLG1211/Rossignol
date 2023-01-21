using EF_Model.Entities;
using Model.Business.Users;

namespace EF_Model.Utils
{
    public static class LocalUserConverter
    {
        public static LocalUser LToModel(this LocalUserEntity entity)
            =>  new LocalUser(new Guid(entity.Uid), entity.Password, EntryConverter.ToModels(entity.OwnedEntries ?? new List<EntryEntity>()).ToList());

        public static IEnumerable<LocalUser> LToModels(this IEnumerable<LocalUserEntity> entities)
        => entities.Select(e => e.LToModel());

        public static LocalUserEntity LToEntity(this LocalUser model)
        =>  new LocalUserEntity
        {
                Uid = model.Uid.ToString(),
                Password = model.Password,
                //Entries = ProprietaryEntryConverter.ToEntities(model.ownedEntries)
            };

        public static IEnumerable<LocalUserEntity> LToEntities(this IEnumerable<LocalUser> users)
        => users.Select(m => m.LToEntity());
    }
}
