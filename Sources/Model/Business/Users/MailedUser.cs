using Model.Business.Entries;

namespace Model.Business.Users
{
    public abstract class MailedUser : AbstractUser
    {
        public string Mail { get; protected set; }

        protected MailedUser(Guid uid, string mail, string password, List<Entry> entries) : base(uid, password, entries)
        {
            if (mail != null)
            {
                Mail = mail;
            }
            else
            {
                throw new ArgumentNullException(nameof(mail));
            }
        }

        protected MailedUser(string mail, string password, List<Entry> entries) : base(password, entries)
        {
            if (mail != null)
            {
                Mail = mail;
            }
            else
            {
                throw new ArgumentNullException(nameof(mail));
            }
        }

        protected MailedUser(string mail, string password) : base(password)
        {
            if (mail != null)
            {
                Mail = mail;
            }
            else
            {
                throw new ArgumentNullException(nameof(mail));
            }
        }
    }
}
