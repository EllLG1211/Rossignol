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

        public ICollection<ConnectedUserEntity>? SharedWith { get; set; }   //many to many

        public override int GetHashCode()
        {
            return Uid.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if(obj == null || obj is not EntryEntity) return false;

            EntryEntity other = obj as EntryEntity;

            return other.Uid == this.Uid;
        }
    }
}
