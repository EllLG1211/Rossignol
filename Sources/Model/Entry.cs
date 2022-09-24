using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Entry : IEquatable<Entry>
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="app"></param>
        /// <param name="note"></param>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Verify if <i>this</i> is equal than <i>other</i>. 
        /// This method donc take care of the Uid or the referance and test only with Login, Password and App.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool IEquatable<Entry>.Equals(Entry? other)
        {
            if (other == null)
                return false;
            if (!Login.Equals(other.Login) || !Password.Equals(other.Password) || !App.Equals(other.App))
                return false;
            return true;
            
        }

        /// <summary>
        /// Verify if <i>this</i> <i>Uid</i> is equal than <i>other</i> one.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool EqualsUid(Entry? other)
        {
            if(other == null ) return false;
            return Uid.Equals(other.Uid);
        }
    }
}
