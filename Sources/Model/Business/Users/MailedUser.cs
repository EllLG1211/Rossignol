using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Users
{
    public class MailedUser : AbstractUser
    {
        public string Mail { get; protected set; }

        public MailedUser(string mail, string password) : base(password)
        {
            Mail = mail;
        }
    }
}
