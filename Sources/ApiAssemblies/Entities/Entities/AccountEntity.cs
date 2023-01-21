namespace ApiEntities
{
    /// <summary>
    /// WARNING! NEVER have a response return this. This should only be in the body of a request.
    /// </summary>
    public class AccountEntity
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
