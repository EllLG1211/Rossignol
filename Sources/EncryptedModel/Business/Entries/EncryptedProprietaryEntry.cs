using Model.Business.Users;

namespace EncryptedModel.Business.Entries
{
    public class EncryptedProprietaryEntry : EncryptedEntry{
        public List<MailedUser> SharedWith { get; set; }
        public EncryptedProprietaryEntry(string encryptionType, string Uid, byte[] EncryptedLogin, byte[] EncryptedPassword, byte[] EncryptedApp, byte[] EncryptedNote, List<MailedUser>? SharedWith = null) 
            : base(encryptionType, Uid, EncryptedLogin, EncryptedPassword, EncryptedApp, EncryptedNote)
        {
            this.SharedWith = SharedWith ?? new List<MailedUser>();
        }
    }
}
