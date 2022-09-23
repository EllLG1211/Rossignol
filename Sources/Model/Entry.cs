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
        protected Guid Uid { get; init; }

        /// <summary>
        /// Login's app
        /// It can be nickname, email,...
        /// </summary>
        public string Login { get; protected set; }

        /// <summary>
        /// Password used on the app.
        /// This property will have to be redifined; encrypted, it will not be of type string. Probably a byte[]
        /// </summary>
        protected string Password { get; set; }

        /// <summary>
        /// Password's App/Website
        /// </summary>
        public string App { get; protected set; }

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
        public string? Note { get; protected set; }

        protected Entry(string login, string password, string app, string? note)
        {
            if (String.IsNullOrEmpty(login))
            {
                throw new ArgumentNullException(nameof(login));
            }
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (String.IsNullOrEmpty(app))
            {
                throw new ArgumentNullException(nameof(app));
            }

            Uid = Guid.NewGuid();
            Login = login;
            Password = password;
            App = app;
            Note = note ?? string.Empty;
        }
    }
}
