using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
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
        /// Master password of the user.
        /// </summary>
        public String Password { get; protected set; }

        protected AbstractUser(string password)
        {
            Password = password;
        }


        /// <summary>
        /// Add an entry to the user.
        /// </summary>
        /// <param name="entry">Entry to add</param>
        public void AddEntry(Entry? entry)
        {
            if(entry != null)
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
            _entries.Remove(entry);
        }


    }
}
