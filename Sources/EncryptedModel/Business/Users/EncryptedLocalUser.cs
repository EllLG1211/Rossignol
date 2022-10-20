namespace EncryptedModel.Business.Users
{
    public class EncryptedLocalUser : EncryptedUser
    {
        public EncryptedLocalUser(string encryptionType, string uid, byte[] encryptedPassword) 
            : base(encryptionType, uid, encryptedPassword) { }
    }
}
