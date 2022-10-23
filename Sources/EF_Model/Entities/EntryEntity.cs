namespace EF_Model.Entities
{
    public class EntryEntity
    {
        public LocalUserEntity Owner { get; set; }  //one to many
        public string EncryptionType { get; set; }

        public string Uid { get; set; }

        public byte[] Login { get; set; }

        public byte[] Password { get; set; }

        public byte[] App { get; set; }

        public byte[]? Note { get; set; }

        public ICollection<ConnectedUserEntity>? SharedWith { get; set; }
    }
}
