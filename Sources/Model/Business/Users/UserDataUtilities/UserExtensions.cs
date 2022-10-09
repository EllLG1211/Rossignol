using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Users.UserDataUtilities
{
    public static class UserExtensions
    {
        public static string ConcatToString(this IEnumerable<MailedUser> list)
        {
            string toReturn = "";
            if (list == null) return toReturn;
            foreach(MailedUser user in list)
                toReturn += (user.Mail + "\t");
            return toReturn;
        }

        public static List<MailedUser> ToMailedUserList(this string input)
        {
            List<MailedUser> toreturn = new List<MailedUser>();
            if (input == null) return toreturn;

            String[] arr = input.Split('\t');
            
            foreach(string str in arr)
            {
                if(str != "" && str.Contains('@'))
                    toreturn.Add(new MailedUser(str, ""));
            }

            return toreturn;
        }
    }
}