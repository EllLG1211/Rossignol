using System;
using System.Collections.Generic;
using System.Linq;
using EF_Model.Entities;
using Model.Business.Entries;

namespace EF_Model.Utils
{
    public static class ProprietaryEntryExtension
    {
        public static ProprietaryEntry ToModel(this ProprietaryEntityEntity entity)
            => new ProprietaryEntry(entity.Uid, entity.Login, entity.Password, entity.App, entity.Note);

        public static IEnumerable<ProprietaryEntry> ToModels(this IEnumerable<ProprietaryEntryEntity> entities)
            => entities.Select(e => e.ToModel());

        public static ProprietaryEntryEntity ToEntity(this ProprietaryEntry model)
            => new ProprietaryEntryEntity
            {
                Uid = model.Uid;
                Login = model.Login;
                Password = model.Password;
                App = model.App;
                Note = model.App;
            };

        public static IEnumerable<ProprietaryEntryEntity> ToEntities(this IEnumerable<ProprietaryEntry> models)
            => models.Select(m => m.ToEntity());
    }
}
