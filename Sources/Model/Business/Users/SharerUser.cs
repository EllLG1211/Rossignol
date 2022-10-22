using Model.Business.Entries;

namespace Model.Business.Users
{
    public class SharerUser : MailedUser
    {
        public SharerUser(Guid uid, string mail, string password, List<Entry>? entries) : base(uid, mail, password, entries) { }

        public SharerUser(string mail, string password, List<Entry>? entries) : this(Guid.NewGuid(), mail, password, entries) { }

        public SharerUser(string mail, string password) : this(Guid.NewGuid(), mail, password, null) { }
    }
}
