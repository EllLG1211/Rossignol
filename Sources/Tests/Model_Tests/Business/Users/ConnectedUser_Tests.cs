using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Model.Business.Users;
using Model;
using System.Net.Mail;
using Model.Business.Entries;

namespace Model_Tests.Business.Users
{
    public class ConnectedUser_Tests
    {
        [Fact]
        public void Constructor_ShouldAssignEmail()
        {
            string mail = "test@test.com";
            ConnectedUser user = new(mail, "1234");
            Assert.True(mail.Equals(user.Mail));
        }

        [Fact]
        public void Constructor_ShouldAssignPassword()
        {
            string mail = "test@test.com";
            string password = "1234";
            ConnectedUser user = new(mail, password);
            Assert.True(password.Equals(user.Password));
        }

        [Fact]
        public void MailSetter_ShouldAssignValue()
        {
            string mail = "test@test.com";
            ConnectedUser user = new("adresse@url.com", "1234");
            user.Mail = mail;
        }

        [Fact]
        public void PasswordSetter_ShouldAssignValue()
        {
            string password = "test";
            ConnectedUser user = new("adresse@url.com", "1234");
            user.Password = password;
        }
    }
}