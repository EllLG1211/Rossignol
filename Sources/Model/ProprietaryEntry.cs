using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProprietaryEntry : Entry
    {
        protected long Uid { get; init; }

        public string Login { get; set; }

        protected string Password { get; set; }

        public string Website { get; set; }

        public string Note { get; set; }

        public ProprietaryEntry(long uid, string login, string password, string website, string note)
        {
            Uid = uid;
            Login = login;
            Password = password;
            Website = website;
            Note = note ?? string.Empty;
        }

        public ProprietaryEntry(long uid, string login, string password, string website)
            : this(uid, login, password, website, string.Empty) { }
    }
}
