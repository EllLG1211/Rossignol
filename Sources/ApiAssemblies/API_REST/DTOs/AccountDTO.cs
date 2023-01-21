namespace API_REST.DTOs
{
    public class AccountDTO
    {
        public Guid Uid { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }

        public List<EntryDTO>? Entries { get; set; }

        //public IEnumerable<SharedEntryDTO>? SharedEntries { get; set; }
    }
}
