using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries
{
    /// <summary>
    /// Entry in the receiver password side
    /// </summary>
    public class SharedEntry : Entry
    {
        public SharedEntry(Guid uid, string login, string password, string app, string? note) : base(uid, login, password, app, note)
        {
        }

        public SharedEntry(string login, string password, string app, string? note) : base(login, password, app, note)
        {
        }

        public SharedEntry(string login, string password, string app)
            : this(login, password, app, string.Empty) { }


        public SharedEntry(Guid uid, string login, string password, string app) : this(uid, login, password, app, null)
        {
        }
    }
}
