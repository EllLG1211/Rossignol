using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.User
{
    public class Sharer : AbstractUser
    {
        public Sharer(string email) : base(email)
        {
        }
    }
}
