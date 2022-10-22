using Model.Business.Entries;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business
{
    public class Manager
    {
        public AbstractUser? ConnectedUser { get; private set; }

        /// <summary>
        /// Login a <i>ConnectedUser</i>.
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        public void Login(string mail, string password, List<Entry> entries)
        {
            ConnectedUser = new ConnectedUser(mail, password, entries);
        }

        public void Login(string mail, string password)
        {
            Login(mail, password, new List<Entry>());
        }

        /// <summary>
        /// Login a <i>LocalUser</i>.
        /// </summary>
        /// <param name="password"></param>
        public void Login(string password, List<Entry> entries)
        {
            ConnectedUser = new LocalUser(password, entries);
        }

        public void Login(string password)
        {
            Login(password, new List<Entry>());
        }

        /// <summary>
        /// Register the user for <i>ConnectedUser</i>.
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <remarks>Throws a ArgumentNullException if one of argument is null.
        /// Throws a ArgumentException if password and confirm password are not equal.</remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException">Throwed when <i>password</i> and <i>confirmPassword</i> are not equal.</exception>
        public AbstractUser Signin(string mail, string password, string confirmPassword)
        {
            if(String.IsNullOrEmpty(mail))
            {
                throw new ArgumentNullException(nameof(mail));
            }
            else if(String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
            else if(String.IsNullOrEmpty(confirmPassword)) 
            { 
                throw new ArgumentNullException(nameof(confirmPassword)); 
            }
            else if(!password.Equals(confirmPassword))
            {
                throw new ArgumentException("Password does not equal confirmPassword");
            }

            return new ConnectedUser(mail, password);
        }

        /// <summary>
        /// Register the user for <i>LocalUser</i>.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <remarks>Throws a ArgumentNullException if one of argument is null.
        /// Throws a ArgumentException if password and confirm password are not equal.</remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public AbstractUser Signin(string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
            else if (String.IsNullOrEmpty(confirmPassword))
            {
                throw new ArgumentNullException(nameof(confirmPassword));
            }
            else if (!password.Equals(confirmPassword))
            {
                throw new ArgumentException("Password does not equal confirmPassword");
            }

            return new LocalUser(password);
        }

        /// <summary>
        /// Create an entry for the ConnectedUser
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="app"></param>
        /// <param name="note"></param>
        /// <remarks>Throws a NullReferenceException if ConnectedUser is null.</remarks>
        /// <exception cref="NullReferenceException"></exception>
        public void CreateEntryToConnectedUser(string login, string password, string app, string? note)
        {
            ConnectedUser.AddEntry(new ProprietaryEntry(login, password, app, note));
        }

        /// <summary>
        /// Give a SharedEntry to the user.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="app"></param>
        /// <param name="note"></param>
        /// <remarks>Throws a NullReferenceException if ConnectedUser is null.</remarks>
        /// <exception cref="NullReferenceException">Throwed if ConnectedUser is null.</exception>
        public void ShareEntryWithConnectedUser(string login, string password, string app, string? note)
        {
            ConnectedUser.AddEntry(new SharedEntry(login, password, app, note));
        }

        /// <summary>
        /// Log out the user
        /// </summary>
        public void logOut()
        {
            ConnectedUser = null;
        }
    }
}
