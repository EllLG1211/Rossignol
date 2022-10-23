using EncryptedModel.Business.Entries;

namespace EncryptedModel.Business.Users
{
    public class EncryptedConnectedUser : EncryptedUser
    {
        public byte[] EncryptedMail { get; set; }

        public List<EncryptedSharedEntry>? encryptedSharedWith { get; set; }    //entries that are being shared with this user

        public EncryptedConnectedUser(string encryptionType, string uid, byte[] mail ,byte[] encryptedPassword, List<EncryptedProprietaryEntry>? encryptedEntries = null, List<EncryptedSharedEntry>? encryptedSharedWith = null) 
            : base(encryptionType, uid, encryptedPassword, encryptedEntries)
        {
            if (mail == null) { throw new ArgumentNullException(nameof(mail)); }
            this.encryptedSharedWith = encryptedSharedWith ?? new List<EncryptedSharedEntry>();

            EncryptedMail = mail;
        }
    }
}
