using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Users.UserDataUtilities
{
    public static class UserExtensions
    {
        public static byte[]? ToBytes(this List<MailedUser> list)
        {
            if (list == null) return null;
            byte[] toreturn = list.SelectMany(str => Encoding.UTF8.GetBytes(str + "\t")).ToArray();
            return toreturn;
        }

        public static List<MailedUser> ToMailedUserList(this byte[] bytes)
        {
            List<MailedUser> toreturn = new List<MailedUser>();
            if (bytes == null) return toreturn;

            String str = Encoding.UTF8.GetString(bytes);
            String[] arr = str.Split('\t');

            return toreturn;
        }
    }
}
