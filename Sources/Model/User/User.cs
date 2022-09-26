using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.User
{
    public class User : AbstractUser
    {
        public string MasterPassword { get; protected set; }
        public User(string email) : base(email)
        {
            
        }

        /// <summary>
        /// Adds an entry to the user's entries
        /// </summary>
        /// <param name="entry">the entry to add</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddEntry(Entry entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            entries.Add(entry);
        }

        /// <summary>
        /// Removes an entry from the user's entries
        /// </summary>
        /// <param name="entry">the entry to remove</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void RemoveEntry(Entry entry)
        {
            if(entry == null) throw new ArgumentNullException(nameof(entry));
            if(!entries.Contains(entry)) throw new ArgumentException("The entry "+nameof(entry)+" is not part of this user's entries.");
            entries.Remove(entry);
        }
    }
}
