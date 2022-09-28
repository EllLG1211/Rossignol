﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Model.User
{
    public abstract class AbstractUser : IEquatable<AbstractUser>
    {
        /// <summary>
        /// The user's email
        /// </summary>
        public MailAddress email { get; protected set; }


        /// <summary>
        /// The user's entries
        /// </summary>
        public List<Entry> entries { get; protected set; } = new List<Entry>();

        /// <summary>
        /// Checks if two users are equal
        /// </summary>
        bool IEquatable<AbstractUser>.Equals(AbstractUser? other)
        {
            return email.Equals(other?.email);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="email">the user's email</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected AbstractUser(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            this.email = new MailAddress(email);
        }
    }
}
