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

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not MailedUser) return false;
            return Mail.Equals((obj as MailedUser).Mail);
        }

        public override int GetHashCode()
        {
            return Uid.GetHashCode() * 17
             + Password.GetHashCode() * 17 ^ 2
             + Mail.GetHashCode() * 17 ^ 4
             + GetType().GetHashCode();
        }
    }
}
