using Model.Business.Entries;

namespace Model.Business.Users
{
    public abstract class MailedUser : AbstractUser, IEquatable<object>
    {
        public string Mail { get; protected set; }

        protected MailedUser(Guid uid, string mail, string password, List<Entry>? entries) : base(uid, password, entries)
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

        public override bool Equals(object? other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other is not MailedUser) return false;
            return Mail.Equals((other as MailedUser).Mail);
        }
    }
}
