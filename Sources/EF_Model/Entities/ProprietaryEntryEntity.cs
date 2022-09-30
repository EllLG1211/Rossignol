using System;

namespace EF_Model.Entities
{
    public class ProprietaryEntryEntity
    {
        public Guid Uid { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string App { get; set; }

        public string? Note { get; set; }
    }
}
