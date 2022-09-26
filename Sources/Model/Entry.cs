using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Entry
    {
        protected long Uid { get; init; }

        protected string Login { get; set; } = "machin";

        protected string Password { get; set; }

        protected string Website { get; set; } = "truc";

        public string Label
        {
            get => $"{Website} - {Login}";
        }

        protected string Note { get; set; }
    }
}
