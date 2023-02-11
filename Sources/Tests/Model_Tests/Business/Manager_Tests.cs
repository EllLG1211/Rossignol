using Data;
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
        private IDataManager _dataManager = new Stub();

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
            Manager manager = new Manager(_dataManager);
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
            Manager manager = new Manager(_dataManager);
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
            Manager manager = new Manager(_dataManager);
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
            Manager manager = new Manager(_dataManager);
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
            Manager manager = new Manager(_dataManager);
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
            Manager manager = new Manager(_dataManager);
            AbstractUser user = manager.Signin("1234", "1234");
            Assert.IsType<LocalUser>(user);
        }
        #endregion

        #region Login ConnectedUser
        [Fact]
        public void Login_ConnectedUser_ShouldInstantiateAConnectedUserInManager()
        {
            Manager manager = new Manager(_dataManager);
            manager.Login("test@test.com", "1234");
            Assert.IsType<ConnectedUser>(manager.LoggedIn);
        }
        #endregion
        //TODO: check if this one still needs to be commented out
        /*#region Login LocalUser
        [Fact]
        public void Login_LocalUser_ShouldInstantiateALocalUserInManager()
        {
            Manager manager = new Manager(_dataManager);
            manager.Login("1234");
            Assert.IsType<LocalUser>(manager.ConnectedUser);
        }
        #endregion*/ //test plus possible avec le stub

        #region CreateEntryToConnectedUser
        [Fact]
        public void CreateEntryToConnectedUser_ShouldAddProprietaryEntryToConnectedUser()
        {
            var manager = new Manager(_dataManager);
            manager.Login("test@test.com", "1234");
            manager.CreateEntryToConnectedUser("test@test.com","test", "1234", "discord", null);
            if (manager.LoggedIn?.Entries.Count() == 1)
            {
                foreach (Entry entry in manager.LoggedIn.Entries)
                {
                    Assert.IsType<ProprietaryEntry>(entry);
                }
            }
        }

        [Fact]
        public void CreateEntryToConnectedUser_ShouldThrowNullReferenceException()
        {
            var manager = new Manager(_dataManager);
            Assert.Throws<NullReferenceException>(() => { manager.CreateEntryToConnectedUser("mail@login.com", "mail@login.com", "password", "app", "note"); });
        }
        #endregion

        #region ShareEntryWithConnectedUser
        [Fact]
        public void ShareEntryWith_ShouldAddSharedEntryToConnectedUser()
        {
            var manager = new Manager(_dataManager);
            manager.Login("test@test.com", "1234");
            ProprietaryEntry entry = new ProprietaryEntry("test@test.com", "test", "1234", "discord", null);
            manager.ShareEntryWith(entry, "test@test.com", "1234");
            Assert.Contains(entry, _dataManager.GetEntries(manager.LoggedIn));
        }
        #endregion

        #region Logout
        [Fact]
        public void Logout_ShouldUninstantiate()
        {
            var manager = new Manager(_dataManager);
            manager.Login("test@test.com", "1234");
            manager.logOut();
            Assert.Null(manager.LoggedIn);
        }
        #endregion

        [Fact]
        public void RemoveEntry_ShouldRemoveEntry()
        {
            var manager = new Manager(_dataManager);
            manager.Login("test@test.com", "1234");
            Entry entry = manager.LoggedIn.Entries.ToArray()[0];
            manager.RemoveEntry(entry);
            Assert.DoesNotContain(entry, manager.LoggedIn.Entries);
        }

        [Fact]
        public void UnshareEntryTo()
        {
            var manager = new Manager(_dataManager);
            manager.Login("test@test.com", "1234");
            ProprietaryEntry entry = (ProprietaryEntry)manager.LoggedIn.Entries.ToArray()[0];
            MailedUser user = entry.SharedWith.ToArray()[0];
            entry.UnshareToUser(user);
            Assert.DoesNotContain(user, entry.SharedWith);
        }
    }
}
