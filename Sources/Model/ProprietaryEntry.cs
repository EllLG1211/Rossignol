using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProprietaryEntry : Entry
    {

        public new string Login 
        { 
            get => base.Login;
            set => base.Login = value; 
        }

        public new string App
        {
            get => base.App;
            set => base.App = value;
        }

        public new string? Note
        { 
            get => base.Note; 
            set => base.Note = value; 
        }

        public ProprietaryEntry(long uid, string login, string password, string website, string note)
        {
            Uid = uid;
            Login = login;
            Password = password;
            App = website;
            Note = note ?? string.Empty;
        }

        public ProprietaryEntry(long uid, string login, string password, string website)
            : this(uid, login, password, website, string.Empty) { }
    }
}
