using Model.Business.Entries;

namespace Model.Business.Users
{
    public class SharerUser : MailedUser
    {
        public SharerUser(Guid uid, string mail, string password, List<Entry> entries) : base(uid, mail, password, entries) { }

        public SharerUser(string mail, string password, List<Entry> entries) : base(mail, password, entries) { }

        public SharerUser(string mail, string password) : base(mail, password) { }
    }
}
