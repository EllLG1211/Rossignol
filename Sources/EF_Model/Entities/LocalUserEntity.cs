namespace EF_Model.Entities
{
    public class LocalUserEntity
    {
        public String EncryptionType { get; set; }

        public String Uid { get; set; }

        public byte[] Password { get; set; }
    }
}
