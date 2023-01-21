using System.Diagnostics.CodeAnalysis;

namespace EF_Model.Entities
{
    public class ConnectedUserEntity : LocalUserEntity
    {
        public virtual ICollection<EntryEntity> SharedWith { get; set; }    //many to many
        public string Mail { get; set; }
    }
}
