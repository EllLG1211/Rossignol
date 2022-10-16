namespace EF_Model.Entities
{
    public class EntryEntity
    {
        public String EncryptionType { get; set; }

        public String Uid { get; set; }

        public byte[] Login { get; set; }

        public byte[] Password { get; set; }

        public byte[] App { get; set; }

        public byte[] Note { get; set; }
    }
}
