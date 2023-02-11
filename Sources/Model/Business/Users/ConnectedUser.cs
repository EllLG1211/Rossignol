using Model.Business.Entries;
using System.Collections.ObjectModel;

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

        /// <summary>
        /// The entries that are shared with the user
        /// </summary>
        private readonly List<SharedEntry> _sharedEntries = new List<SharedEntry>();
        public IEnumerable<SharedEntry> SharedEntries => new ReadOnlyCollection<SharedEntry>(_sharedEntries);
        public void AddShared(SharedEntry s) { _sharedEntries.Add(s); }
        public void RemoveShared(SharedEntry s) { _sharedEntries.Remove(s); }

        /// <summary>
        /// Utility method used to unshare the original entry (after removing the shared copy from the user that it was shared to)
        /// </summary>
        /// <param name="entryGuid"></param>
        /// <param name="mUser"> targeted user </param>
        /// <returns></returns>
        public bool ApplyUnshareToOriginalEntry(Guid entryGuid, ConnectedUser mUser)
        {
            ((ProprietaryEntry)_entries.Find(entry => entry.Uid.Equals(entryGuid))).UnshareToUser(mUser);
            return true;
        }

        public ConnectedUser(Guid uid, string mail, string password, List<Entry>? entries) 
            : base(uid, mail, password, entries) { }

        public ConnectedUser(string mail, string password, List<Entry>? entries) 
            : this(Guid.NewGuid(), mail, password, entries) { }

        public ConnectedUser(string mail, string password) 
            : this(Guid.NewGuid(), mail, password, null) { }
    }
}
