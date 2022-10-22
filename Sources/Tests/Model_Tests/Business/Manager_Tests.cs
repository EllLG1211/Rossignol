using Model.Business;
using Model.Business.Entries;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Model_Tests.Business
{
    public class Manager_Tests
    {
        #region Signin a ConnectedUser
        [Theory]
        [InlineData(true, null, "1234", "1234")]
        [InlineData(true, "test@test.com", null, "1234")]
        [InlineData(true, "test@test.com", "1234", null)]
        [InlineData(true, "", "1234", "1234")]
        [InlineData(true, "test@test.com", "", "1234")]
        [InlineData(true, "test@test.com", "1234", "")]
        [InlineData(false, "test@test.com", "1234", "1234")]
        [InlineData(false, "test@test.com", "1234", "12345")]
        public void Signin_ConnectedUser_ShouldThrowArgumentNullException(bool expected, string mail, string password, string confirmPassword)
        {
            Manager manager = new Manager();
            if (expected)
            {
                Assert.Throws<ArgumentNullException>(() => { manager.Signin(mail, password, confirmPassword); });
                return;
            } else
            {
                try
                {
                    manager.Signin(mail, password, confirmPassword);
                }
                catch (Exception)
                {
                    Assert.False(expected);
                }
            }
        }

        [Theory]
        [InlineData(false, null, "1234", "1234")]
        [InlineData(false, "test@test.com", null, "1234")]
        [InlineData(false, "test@test.com", "1234", null)]
        [InlineData(false, "", "1234", "1234")]
        [InlineData(false, "test@test.com", "", "1234")]
        [InlineData(false, "test@test.com", "1234", "")]
        [InlineData(false, "test@test.com", "1234", "1234")]
        [InlineData(true, "test@test.com", "1234", "12345")]
        public void Signin_ConnectedUser_ShouldThrowArgumentException(bool expected, string mail, string password, string confirmPassword)
        {
            Manager manager = new Manager();
            if (expected)
            {
                Assert.Throws<ArgumentException>(() => { manager.Signin(mail, password, confirmPassword); });
                return;
            }
            else
            {
                try
                {
                    manager.Signin(mail, password, confirmPassword);
                }
                catch (Exception)
                {
                    Assert.False(expected);
                }
            }
        }

        [Fact]
        public void Signin_ConnectedUser_ShouldReturnConnectedUser()
        {
            Manager manager = new Manager();
            AbstractUser user = manager.Signin("test@test.com", "1234", "1234");
            Assert.IsType<ConnectedUser>(user);
        }
        #endregion

        #region Signin a LocalUser
        [Theory]
        [InlineData(true, null, "1234")]
        [InlineData(true, "1234", null)]
        [InlineData(true, "", "1234")]
        [InlineData(true, "1234", "")]
        [InlineData(false, "1234", "1234")]
        [InlineData(false, "1234", "12345")]
        public void Signin_LocalUser_ShouldThrowArgumentNullException(bool expected, string password, string confirmPassword)
        {
            Manager manager = new Manager();
            if (expected)
            {
                Assert.Throws<ArgumentNullException>(() => { manager.Signin(password, confirmPassword); });
                return;
            }
            else
            {
                try
                {
                    manager.Signin(password, confirmPassword);
                }
                catch (Exception)
                {
                    Assert.False(expected);
                }
            }
        }

        [Theory]
        [InlineData(false, "1234", "1234")]
        [InlineData(false,  null, "1234")]
        [InlineData(false, "1234", null)]
        [InlineData(false, "", "1234")]
        [InlineData(false, "1234", "")]
        [InlineData(true, "1234", "12345")]
        public void Signin_LocalUser_ShouldThrowArgumentException(bool expected, string password, string confirmPassword)
        {
            Manager manager = new Manager();
            if (expected)
            {
                Assert.Throws<ArgumentException>(() => { manager.Signin(password, confirmPassword); });
                return;
            }
            else
            {
                try
                {
                    manager.Signin(password, confirmPassword);
                }
                catch (Exception)
                {
                    Assert.False(expected);
                }
            }
        }

        [Fact]
        public void Signin_LocalUser_ShouldReturnConnectedUser()
        {
            Manager manager = new Manager();
            AbstractUser user = manager.Signin("1234", "1234");
            Assert.IsType<LocalUser>(user);
        }
        #endregion

        #region Login ConnectedUser
        [Fact]
        public void Login_ConnectedUser_ShouldInstantiateAConnectedUserInManager()
        {
            Manager manager = new Manager();
            manager.Login("test@test.com", "1234");
            Assert.IsType<ConnectedUser>(manager.ConnectedUser);
        }

        [Fact]
        public void Login_Entry_ConnectedUser_ShouldGiveList()
        {
            Manager manager = new Manager();
            List<Entry> entries = new List<Entry>();
            Entry entry = new ProprietaryEntry("test", "1234", "test");
            entries.Add(entry);
            manager.Login("test@test.com", "1234", entries);
            Assert.Contains(entry, manager.ConnectedUser.Entries);
        }
        #endregion

        #region Login LocalUser
        [Fact]
        public void Login_LocalUser_ShouldInstantiateALocalUserInManager()
        {
            Manager manager = new Manager();
            manager.Login("1234");
            Assert.IsType<LocalUser>(manager.ConnectedUser);
        }

        [Fact]
        public void Login_Entry_LocalUser_ShouldGiveList()
        {
            Manager manager = new Manager();
            List<Entry> entries = new List<Entry>();
            Entry entry = new ProprietaryEntry("test", "1234", "test");
            entries.Add(entry);
            manager.Login("1234", entries);
            Assert.Contains(entry, manager.ConnectedUser.Entries);
        }
        #endregion

        #region CreateEntryToConnectedUser
        [Fact]
        public void CreateEntryToConnectedUser_ShouldAddProprietaryEntryToConnectedUser()
        {
            var manager = new Manager();
            manager.Login("1234");
            manager.CreateEntryToConnectedUser("test", "1234", "discord", null);
            if (manager.ConnectedUser?.Entries.Count() == 1)
            {
                foreach (Entry entry in manager.ConnectedUser.Entries)
                {
                    Assert.IsType<ProprietaryEntry>(entry);
                }
            }
        }

        [Fact]
        public void CreateEntryToConnectedUser_ShouldThrowNullReferenceException()
        {
            var manager = new Manager();
            Assert.Throws<NullReferenceException>(() => { manager.CreateEntryToConnectedUser("login", "password", "app", "note"); });
        }
        #endregion

        #region ShareEntryWithConnectedUser
        [Fact]
        public void ShareEntryWithConnectedUser_ShouldAddSharedEntryToConnectedUser()
        {
            var manager = new Manager();
            manager.Login("1234");
            manager.ShareEntryWithConnectedUser("test", "1234", "discord", null);
            if (manager.ConnectedUser?.Entries.Count() == 1)
            {
                foreach (Entry entry in manager.ConnectedUser.Entries)
                {
                    Assert.IsType<SharedEntry>(entry);
                }
            }
        }

        [Fact]
        public void ShareEntryWithConnectedUser_ShouldThrowNullReferenceException()
        {
            var manager = new Manager();
            Assert.Throws<NullReferenceException>(() => { manager.ShareEntryWithConnectedUser("login", "password", "app", "note"); });
        }
        #endregion

        #region Logout
        [Fact]
        public void Logout_ShouldUninstantiate()
        {
            var manager = new Manager();
            manager.Login("login", "password");
            manager.logOut();
            Assert.Null(manager.ConnectedUser);
        }
        #endregion
    }
}
