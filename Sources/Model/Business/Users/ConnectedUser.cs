using Model.Business.Entries;

namespace Model.Business.Users
{
    public class ConnectedUser : MailedUser
    {
        public new string Mail
        {
            get => base.Mail;
            set => base.Mail = value;
        }

        public new string Password
        {
            get => base.Password;
            set => base.Password = value;
        }

        public ConnectedUser(Guid uid, string mail, string password, List<Entry> entries) : base(uid, mail, password, entries) { }

        public ConnectedUser(string mail, string password, List<Entry> entries) : base(mail, password, entries) { }

        public ConnectedUser(string mail, string password) : base(mail, password) { }
    }
}
