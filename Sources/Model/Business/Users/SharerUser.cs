using Model.Business.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Users
{
    public class SharerUser : MailedUser
    {
        public SharerUser(string mail, string password) : base(mail, password)
        {
        }
        
        public SharerUser(string mail, string password, List<Entry> entries) : base(mail, password, entries)
        {
        }
    }
}
