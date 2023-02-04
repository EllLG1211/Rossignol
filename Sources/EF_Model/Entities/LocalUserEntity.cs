using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EF_Model.Entities
{
    public class LocalUserEntity : IEquatable<LocalUserEntity>
    {
        //public string EncryptionType { get; set; }

        [MemberNotNullWhen(true, nameof(Uid))]
        [Key]
        public string Uid { get; set; }

        [MemberNotNullWhen(true, nameof(Password))]
        public string Password { get; set; }

        public virtual ICollection<EntryEntity> OwnedEntries { get; set; } //one to many

        public bool Equals(LocalUserEntity? other)
        {
            return other != null && other.Uid == Uid;
        }
    }
}