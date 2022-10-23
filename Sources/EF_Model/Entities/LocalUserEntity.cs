using System.ComponentModel.DataAnnotations;

namespace EF_Model.Entities
{
    public class LocalUserEntity
    {
        public string EncryptionType { get; set; }
        [Key]
        public string Uid { get; set; }

        public byte[] Password { get; set; }

        public virtual ICollection<EntryEntity> Entries { get; set; } //one to many
    }
}