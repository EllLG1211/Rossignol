using EncryptedModel.Business.Entries;

namespace EncryptedModel.Business.Users
{
    public class EncryptedLocalUser : EncryptedUser
    {
        public EncryptedLocalUser(string encryptionType, string uid, byte[] encryptedPassword, List<EncryptedProprietaryEntry>? encryptedEntries = null) 
            : base(encryptionType, uid, encryptedPassword, encryptedEntries) { }
    }
}
