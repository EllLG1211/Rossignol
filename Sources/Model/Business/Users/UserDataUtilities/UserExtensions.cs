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
            if (list == null) return "";
            StringBuilder sb = new StringBuilder();
            foreach (MailedUser user in list)
            {
                sb.Append(user.Mail.ToString());
                sb.Append("\t");
            }
            return sb.ToString();
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