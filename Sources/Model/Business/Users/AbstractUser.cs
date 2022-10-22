using Model.Business.Entries;
using System.Collections.ObjectModel;

namespace Model.Business.Users
{
    public abstract class AbstractUser
    {
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

        protected AbstractUser(string password, List<Entry> entries) : this(Guid.NewGuid(), password, entries) { }

        protected AbstractUser(string password) : this(Guid.NewGuid(), password, new List<Entry>()) { }


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
    }
}
