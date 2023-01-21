namespace API_REST.DTOs
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public string Passord { get; set; }

        public List<EntryDTO>? Entries { get; set; }

        public List<SharedEntryDTO>? SharedEntries { get; set; }
    }
}
