namespace DTOs
{
    public class EntryDTO
    {
        public Guid Uid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string App { get; set; }
        public string? Note { get; set; }
        public AccountDTO Owner { get; set; }
    }
}
