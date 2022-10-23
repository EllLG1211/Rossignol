using System.Diagnostics.CodeAnalysis;

namespace EF_Model.Entities
{
    public class LocalUserEntity
    {
        public string EncryptionType { get; set; }

        [MemberNotNullWhen(true, nameof(Uid))]
        public string Uid { get; set; }

        [MemberNotNullWhen(true, nameof(Password))]
        public byte[] Password { get; set; }

        public virtual ICollection<EntryEntity> Entries { get; set; } //one to many
    }
}