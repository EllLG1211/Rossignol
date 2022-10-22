using System.ComponentModel.DataAnnotations;

namespace EF_Model.Entities
{
    public class EntryEntity
    {
        public string EncryptionType { get; set; }
        [Key]
        public string Uid { get; set; }

        public byte[] Login { get; set; }

        public byte[] Password { get; set; }

        public byte[] App { get; set; }

        public byte[] Note { get; set; }
    }
}
