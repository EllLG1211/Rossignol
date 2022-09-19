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
        /// <summary>
        /// Unique identifier
        /// </summary>
        protected long Uid { get; init; }

        /// <summary>
        /// Login's app
        /// It can be nickname, email,...
        /// </summary>
        protected string Login { get; set; } = "machin";

        /// <summary>
        /// Password used on the app
        /// </summary>
        protected string Password { get; set; }

        /// <summary>
        /// Password's App/Website
        /// </summary>
        protected string App { get; set; } = "truc";

        /// <summary>
        /// Label 
        /// </summary>
        public string Label
        {
            get => $"{App} - {Login}";
        }

        /// <summary>
        /// Note about the entry.
        /// </summary>
        protected string? Note { get; set; }
    }
}
