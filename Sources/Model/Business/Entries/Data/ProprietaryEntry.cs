using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries
{
    public class ProprietaryEntry : Entry
    {
        public ProprietaryEntry(string login, string password, string app, string? note) : base(login, password, app, note)
        {
        }

        public ProprietaryEntry(string login, string password, string app)
            : this(login, password, app, string.Empty) { }
    }
}
