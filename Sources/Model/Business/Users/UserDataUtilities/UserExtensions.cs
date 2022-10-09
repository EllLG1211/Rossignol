using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Users.UserDataUtilities
{
    public static class UserExtensions
    {
        public static string? ConcatToString(this IEnumerable<MailedUser> list)
        {
            if (list == null) return null;
            string? toreturn = list.SelectMany(user => user.Mail + "\t").ToArray().ToString();
            return toreturn;
        }

        public static List<MailedUser> ToMailedUserList(this string str)
        {
            List<MailedUser> toreturn = new List<MailedUser>();
            if (str == null) return toreturn;

            String[] arr = str.Split('\t');

            return toreturn;
        }
    }
}