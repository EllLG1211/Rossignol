using Model.Business.Users;
using Model.Business.Users.UserDataUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Model_Tests.Business.Users.UserDataUtilities
{
    public class UserExtensions_Tests
    {
        [Fact]
        public void TestIEnumExtensions()
        {
            List<MailedUser> lm = new List<MailedUser>();
            lm.Add(new MailedUser("mymail@somemail.com", "123456"));
            lm.Add(new MailedUser("othermail@orange.fr", "987456"));

            IEnumerable<MailedUser> iem =  lm.AsReadOnly();

            string str = iem.ConcatToString();

            Assert.NotEmpty(str);
            Assert.Equal(str, "mymail@somemail.com" + '\t' + "othermail@orange.fr" + '\t');

            List<MailedUser> lm2 =  str.ToMailedUserList();

            Assert.Equal(lm.Count, lm2.Count);
        }
    }
}
