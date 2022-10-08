using Model.Business.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ConnectedUser(string mail, string password) : base(mail, password)
        {
        }

        public ConnectedUser(string mail, string password, List<Entry> entries) : base(mail, password, entries)
        {
        }
    }
}
