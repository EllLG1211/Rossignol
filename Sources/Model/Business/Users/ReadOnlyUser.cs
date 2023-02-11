using Model.Business.Entries;

namespace Model.Business.Users
{
    public class ReadOnlyUser : MailedUser
    {
        public ReadOnlyUser(MailedUser mu) : this(mu.Uid, mu.Mail, mu.Password, mu.Entries.ToList()) { }
        public ReadOnlyUser(Guid uid, string mail, string password, List<Entry>? entries) : base(uid, mail, password, entries) { }

        public ReadOnlyUser(string mail, string password, List<Entry>? entries) : this(Guid.NewGuid(), mail, password, entries) { }

        public ReadOnlyUser(string mail, string password) : this(Guid.NewGuid(), mail, password, null) { }
    }
}
