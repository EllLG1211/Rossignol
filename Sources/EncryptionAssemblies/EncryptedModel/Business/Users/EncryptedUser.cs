using EncryptedModel.Business.Entries;

namespace EncryptedModel.Business.Users
{
    public abstract class EncryptedUser
    {
        public readonly string EncryptionType;
        public string Uid { get; set; }
        public byte[] EncryptedPassword { get; set; }

        public List<EncryptedProprietaryEntry>? ownedEncryptedEntries { get; set; } //entries owned by this user

        protected EncryptedUser(string encryptionType, string uid, byte[] encryptedPassword, List<EncryptedProprietaryEntry>? encryptedEntries = null)
        {
            if (encryptionType == null) throw new ArgumentNullException(nameof(encryptionType));
            if (uid == null) throw new ArgumentNullException(nameof(uid));
            if (encryptedPassword == null) throw new ArgumentNullException(nameof(encryptedPassword));

            this.ownedEncryptedEntries = encryptedEntries ?? new List<EncryptedProprietaryEntry>();

            EncryptionType = encryptionType;
            Uid = uid;
            EncryptedPassword = encryptedPassword;
        }
    }
}
