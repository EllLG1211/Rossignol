namespace EF_Model.Entities
{
    public class EntryEntity
    {
        public LocalUserEntity Owner { get; set; }  //one to many
        //public string EncryptionType { get; set; }
        public string Uid { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string App { get; set; }

        public string? Note { get; set; }

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
