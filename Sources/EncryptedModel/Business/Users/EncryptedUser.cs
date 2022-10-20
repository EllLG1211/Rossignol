namespace EncryptedModel.Business.Users
{
    public abstract class EncryptedUser
    {
        public readonly string EncryptionType;
        public string Uid { get; set; }
        public byte[] EncryptedPassword { get; set; }

        protected EncryptedUser(string encryptionType, string uid, byte[] encryptedPassword)
        {
            if (encryptionType == null) throw new ArgumentNullException(nameof(encryptionType));
            if (uid == null) throw new ArgumentNullException(nameof(uid));
            if (encryptedPassword == null) throw new ArgumentNullException(nameof(encryptedPassword));

            EncryptionType = encryptionType;
            Uid = uid;
            EncryptedPassword = encryptedPassword;
        }
    }
}
