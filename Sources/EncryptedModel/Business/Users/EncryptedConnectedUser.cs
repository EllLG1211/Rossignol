namespace EncryptedModel.Business.Users
{
    public class EncryptedConnectedUser : EncryptedUser
    {
        public byte[] EncryptedMail { get; set; }

        public EncryptedConnectedUser(string encryptionType, string uid, byte[] mail ,byte[] encryptedPassword) 
            : base(encryptionType, uid, encryptedPassword)
        {
            if (mail == null) { throw new ArgumentNullException(nameof(mail)); }

            EncryptedMail = mail;
        }
    }
}
