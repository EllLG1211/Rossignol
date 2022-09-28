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

        public ProprietaryEntry(string login, string password, string app, string note) 
            : base(login, password, app, note){}

        public ProprietaryEntry(string login, string password, string app)
            : this(login, password, app, string.Empty) { }
    }
}
