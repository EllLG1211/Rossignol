﻿using Model.Business.Entries;

namespace Model.Business.Users
{
    /// <summary>
    /// User when he's not logged in.
    /// </summary>
    public class LocalUser : AbstractUser
    {
        public LocalUser(Guid uid, string password, List<Entry> entries) 
            : base(uid, password, entries) { }

        public LocalUser(string password, List<Entry> entries) 
            : base(password, entries) { }

        public LocalUser(string password) 
            : base(password) { }

        public LocalUser(Guid uid, string password) 
            : this(uid, password, null) { }
    }
}
