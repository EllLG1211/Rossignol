using Model.Business.Entries;
using Model.Business.Users.Comparers;
using System.Collections.ObjectModel;

namespace Model.Business.Users
{
    public abstract class AbstractUser : IEquatable<AbstractUser>
    {
        private static UserComparer? _comparer;
        private static UserComparer Comparer => _comparer ??= new UserComparer();
        /// <summary>
        /// The user's entries
        /// </summary>
        private readonly List<Entry> _entries = new List<Entry>();
        public IEnumerable<Entry> Entries => new ReadOnlyCollection<Entry>(_entries);

        /// <summary>
        /// Id for the user.
        /// </summary>
        public Guid Uid { get; protected set; }

        /// <summary>
        /// Master password of the user.
        /// </summary>
        public String Password { get; protected set; }

        protected AbstractUser(Guid uid, string password, IEnumerable<Entry>? entries)
        {
            Uid = uid;
            if (password != null)
            {
                Password = password;
            }
            else
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (entries != null)
            {
                _entries.AddRange(entries);
            }
        }


        /// <summary>
        /// Add an entry to the user.
        /// </summary>
        /// <param name="entry">Entry to add</param>
        public void AddEntry(Entry? entry)
        {
            if (entry != null)
            {
                _entries.Add(entry);
            }
        }

        /// <summary>
        /// Remove an entry of the user.
        /// </summary>
        /// <param name="entry">Entry to delete</param>
        public void RemoveEntry(Entry? entry)
        {
            if (entry != null)
            {
                _entries.Remove(entry);
            }
        }

        //public virtual bool Equals(AbstractUser? other) => Comparer.Equals(this, other);
        public virtual bool Equals(AbstractUser? other)
        {
            if (other == null) return false;
            if (other.Uid != Uid) return false;
            return true;
        }

        public override bool Equals(object? obj)
            => obj is AbstractUser entry && Comparer.Equals(this, entry);

        public override int GetHashCode() => Comparer.GetHashCode(this);
    }
}