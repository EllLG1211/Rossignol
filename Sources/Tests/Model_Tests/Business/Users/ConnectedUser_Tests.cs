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
        /// <summary>
        /// Test if constructor assign Mail.
        /// </summary>
        [Fact]
        public void Constructor_ShouldAssignEmail()
        {
            string mail = "test@test.com";
            ConnectedUser user = new(mail, "1234");
            Assert.Equal(user.Mail, mail);
        }

        /// <summary>
        /// Test if constuctor assign Password
        /// </summary>
        [Fact]
        public void Constructor_ShouldAssignPassword()
        {
            string mail = "test@test.com";
            string password = "1234";
            ConnectedUser user = new(mail, password);
            Assert.Equal(user.Password, password);
        }

        [Fact]
        public void Constructor_ShouldInstantiateEntries()
        {
            AbstractUser user = new ConnectedUser("test@test.com", "1234");
            Assert.NotNull(user.Entries);
        }

        [Fact]
        public void Constructor_ShouldGiveListInstance()
        {
            List<Entry> entries = new List<Entry>();
            Entry entry = new ProprietaryEntry("test", "1234", "app");
            entries.Add(entry);
            AbstractUser user = new ConnectedUser("test@test.com", "1234", entries);
            Assert.Contains(entry, user.Entries);
        }

        /// <summary>
        /// Test if Mail's setter set the value of Mail.
        /// </summary>
        [Fact]
        public void MailSetter_ShouldAssignValue()
        {
            string mail = "test@test.com";
            ConnectedUser user = new("adresse@url.com", "1234");
            user.Mail = mail;
            Assert.Equal(mail, user.Mail);
        }

        /// <summary>
        /// Test if Password's setter set the value of Password
        /// </summary>
        [Fact]
        public void PasswordSetter_ShouldAssignValue()
        {
            string password = "test";
            ConnectedUser user = new("adresse@url.com", "1234");
            user.Password = password;
            Assert.Equal(password, user.Password);
        }
    }
}