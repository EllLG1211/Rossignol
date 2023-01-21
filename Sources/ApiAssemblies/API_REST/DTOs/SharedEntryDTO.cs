namespace API_REST.DTOs
{
    public class SharedEntryDTO
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string App { get; set; }
        public string? Note { get; set; }
        public MailedUserDTO Owner { get; set; }
    }
}
