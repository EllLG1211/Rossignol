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
        public AbstractUser ConnectedUser { get; private set; }

        /*public void Login(string mail, string password)
        {

        }*/

        /// <summary>
        /// Register the user for <i>ConnectedUser</i>.
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <exception cref="ArgumentNullException">Throwed when one of arguments is null.
        ///     The exception contains name of the null field (<i>mail</i>, <i>password</i>, <i>confirmPassword</i>).
        /// </exception>
        /// <exception cref="ArgumentException">Throwed when <i>password</i> and <i>confirmPassword</i> are not equal.</exception>
        public void Signin(string mail, string password, string confirmPassword)
        {
            if(String.IsNullOrEmpty(mail))
            {
                throw new ArgumentNullException("mail");
            }
            else if(String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }
            else if(String.IsNullOrEmpty(confirmPassword)) 
            { 
                throw new ArgumentNullException("confirmPassword"); 
            }
            else if(!password.Equals(confirmPassword))
            {
                throw new ArgumentException("Password does not equal confirmPassword");
            }

            ConnectedUser = new ConnectedUser(mail, password);
        }
    }
}
