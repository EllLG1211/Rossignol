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
        public AbstractUser? LoggedIn { get; private set; }

        private readonly IDataManager _dataManager;
        public IDataManager DataManager => _dataManager;

        public Manager(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        /// <summary>
        /// Login a <i>ConnectedUser</i>.
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        public void Login(string? mail, string password)
        {
            if(mail == null)
            {
                LoggedIn = DataManager.GetUser(mail, password) as LocalUser;
            } else
            {
                LoggedIn = DataManager.GetUser(mail, password) as ConnectedUser;
            }
            
        }

        /// <summary>
        /// Login a <i>LocalUser</i>.
        /// </summary>
        /// <param name="password"></param>
        public void Login(string password)
        {
            Login(null, password);
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

            ConnectedUser user = new ConnectedUser(mail, password);

            DataManager.Register(user);

            return user;
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

            LocalUser user = new LocalUser(password);

            DataManager.Register(user);

            return user;
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
            ProprietaryEntry entry = new ProprietaryEntry(login, password, app, note);
            //LoggedIn.AddEntry(entry); //this was adding two entries, bad
            DataManager.CreateEntryToConnectedUser(LoggedIn, entry);
        }

        public void RemoveEntry(Entry entry)
        {
            //LoggedIn.RemoveEntry(entry);
            DataManager.RemoveEntry(LoggedIn, entry);
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
        public bool ShareEntryWith(ProprietaryEntry entry, string mailUserToShareWith)
        {
            if (!_dataManager.checkUserExists(mailUserToShareWith))
            return false;
            
            MailedUser userToShareWith = new ReadOnlyUser(mailUserToShareWith, "");
            //entry.ShareToUser(userToShareWith);   //useless
            DataManager.ShareEntryWith(entry, userToShareWith);

            return true;
        }

        public void UnshareEntryTo(ProprietaryEntry entry, MailedUser user)
        {
            entry.UnshareToUser(user);
            DataManager.UnshareEntryTo(entry, user);
        }

        /// <summary>
        /// Log out the user
        /// </summary>
        public void logOut()
        {
            LoggedIn = null;
            //_dataManager.clear(); //this flushes everything! we don't want that!!! (this also erases all of our users)
        }

        public void save()
        {
            DataManager.save();
        }
    }
}
