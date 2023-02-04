namespace Model.Api.Entities
{
    public class User
    {
        public User(Guid id, string mail)
        {
            Id = id;
            Mail = mail;
        }

        public Guid Id { get; set; }
        public String Mail { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
