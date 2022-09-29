using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries
{
    public abstract class Entry : IEquatable<Entry>, IEqualityComparer<Entry>
    {
        /*private static EntryComparer? _comparer;
        private static EntryComparer Comparer => _comparer ??= new EntryComparer();*/

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
        public string Password { get; protected set; }

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

        public bool Equals(Entry? other) => Equals(this, other);

        public override bool Equals(object? obj)
            => obj is Entry entry && Equals(this, entry);

        public bool Equals(Entry? x, Entry? y)
        {
            if (x == null || y == null) return x == null && y == null; //must return true if both members are null
            if (x.GetType() != y.GetType()) return false;
            return x.Label.Equals(y.Label) && x.Password.Equals(y.Password);
        }

        public override int GetHashCode() => GetHashCode(this);
        public int GetHashCode([DisallowNull] Entry obj)
            => obj.Label.GetHashCode() * 17
             + obj.Password.GetHashCode() * 17 ^ 2
             + obj.GetType().GetHashCode();
    }
}
