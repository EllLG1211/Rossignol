using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Model;
using System.Diagnostics;
using Model.Business.Entries;
using Newtonsoft.Json.Linq;
using Model.Business.Users;

namespace Model_Tests.Business.Entries
{
    public class ProprietaryEntry_Tests
    {
        /// <summary>
        /// Test if class constructor set the correct values.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="app"></param>
        /// <param name="note"></param>
        /// <param name="noteSuccessExpected"></param>
        [Theory]
        [InlineData(true, "schtroumpf", "truc", "abracadabra")]
        [InlineData(true, "lorem.ipsum@mail.com", "Lorem Ipsum", "abracadabra")]
        [InlineData(false, "schtroumpf", "truc", null)]
        [InlineData(true, "korè@bidule", "Wikipédia", "abracadabra")]
        public void Constructor_ShouldAssignValues(bool noteSuccessExpected, string login, string app, string note)
        {
            ProprietaryEntry entry = new(login, "lorem ipsum", app, note);
            Assert.Equal(login, entry.Login);
            Assert.Equal(app, entry.App);
            if (noteSuccessExpected) Assert.Equal(note, entry.Note);
            else Assert.NotEqual(note, entry.Note);
        }

        /// <summary>
        /// Test if class constructor throw exception when login, password or app is are null.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="app"></param>
        /// <param name="throwSuccessExpected"></param>
        [Theory]
        [InlineData(true, null, "Lorem ipsum", "abracadabra")]
        [InlineData(true, "schtroumpf", null, "abracadabra")]
        [InlineData(true, "schtroumpf", "Lorem ipsum", null)]
        [InlineData(true, null, null, null)]
        [InlineData(false, "schtroumpf", "Lorem ipsum", "Avadra kevadra")]
        public void Constructor_ShouldThrowArgumentNullException(bool throwSuccessExpected, string login, string password, string app)
        {
            if (throwSuccessExpected)
            {
                Assert.Throws<ArgumentNullException>(() => { ProprietaryEntry entry = new(login, password, app); });
                return;
            }
            Assert.False(throwSuccessExpected);
        }

        #region Test constructor with Guid
        [Theory]
        [InlineData(true,false)]
        [InlineData(false, true)]
        public void Constructor_Guid_ShouldThrowArgumentNullException(bool throwSuccessExpected, bool useGuid)
        {
            if (throwSuccessExpected)
            {
                Assert.Throws<ArgumentNullException>(() => 
                {
                    if (useGuid)
                    {
                        ProprietaryEntry entry = new(Guid.NewGuid(), "login", "1234", "app");
                    } else
                    {
                        ProprietaryEntry entry = new(Guid.Empty, "login", "1234", "app");
                    }
                });
                return;
            }
            Assert.False(throwSuccessExpected);
        }
        #endregion

        /*public static IEnumerable<Object[]> Constructor_ShouldThrowArgumentNullException_data()
        {
            yield return new Object[]
            {
                true,
                null,
                "schtroumpf",
                "Lorem ipsum",
                "Avadra kevadra"
            };

        }*/

        /// <summary>
        /// Test if note is reassign when the parameter is not passed.
        /// </summary>
        [Fact]
        public void Constructor_ShouldReassignNote()
        {
            ProprietaryEntry entry = new("loremipsum@gmail.com", "rickroll", "Discord");
            Assert.Equal(string.Empty, entry.Note);
        }

        /// <summary>
        /// Test if <i>Login</i> setter reassign it.
        /// </summary>
        [Fact]
        public void LoginSetter_ShouldReassignLogin()
        {
            string newLogin = "Test";
            ProprietaryEntry entry = new ProprietaryEntry("Ines", "1234", "Photoshop", "Complexe");
            entry.Login = newLogin;
            Assert.Equal(newLogin, entry.Login);
        }

        /// <summary>
        /// Test if <i>App</i> setter reassign it.
        /// </summary>
        [Fact]
        public void AppSetter_ShouldReassignApp()
        {
            string newApp = "Lightroom";
            ProprietaryEntry entry = new ProprietaryEntry("Ines", "1234", "Photoshop", "Complexe");
            entry.App = newApp;
            Assert.Equal(newApp, entry.App);
        }

        /// <summary>
        /// Test if <i>Note</i> setter reassign it.
        /// </summary>
        [Fact]
        public void NoteSetter_ShouldReassignNote()
        {
            string newNote = "Efficace";
            ProprietaryEntry entry = new ProprietaryEntry("Ines", "1234", "Photoshop", "Complexe");
            entry.Note = newNote;
            Assert.Equal(newNote, entry.Note);
        }

        /// <summary>
        /// Test the label getter. 
        /// </summary>
        [Fact]
        public void Label_ShouldReturnLoginAndApp()
        {
            ProprietaryEntry entry = new("loremipsum@gmail.com", "rickroll", "Discord");
            Assert.Equal("Discord - loremipsum@gmail.com", entry.Label);
        }

        /// <summary>
        /// Test if SharedToUser add user in the entry
        /// </summary>
        /// <param name="user"></param>
        [Theory]
        [MemberData(nameof(SharedToUser_ShouldAddUserToSharedWith_Data))]
        public void SharedToUser_ShouldAddUserToSharedWith(MailedUser user)
        {
            ProprietaryEntry entry = new("loremipsum@gmail.com", "rickroll", "Discord");
            entry.ShareToUser(user);
            Assert.Contains(user, entry.GetSharedWith());
        }

        public static IEnumerable<Object[]> SharedToUser_ShouldAddUserToSharedWith_Data()
        {
            yield return new Object[]
            {
                new ConnectedUser("loremipsum@gmail.com","1234")
            };

            yield return new Object[]
            {
                new SharerUser("loremipsum@gmail.com","1234")
            };
        }

        /// <summary>
        /// Test if UnshareToUser work well.
        /// </summary>
        /// <param name="user"></param>
        [Theory]
        [MemberData(nameof(UnsharedToUser_ShouldUserUserFromSharedWith_Data))]
        public void UnsharedToUser_ShouldUserUserFromSharedWith(MailedUser user)
        {
            ProprietaryEntry entry = new("loremipsum@gmail.com", "rickroll", "Discord");
            entry.ShareToUser(user);
            entry.UnshareToUser(user);
            Assert.DoesNotContain(user, entry.GetSharedWith());
        }

        public static IEnumerable<Object[]> UnsharedToUser_ShouldUserUserFromSharedWith_Data()
        {
            #region Test with ConnectedUser
            yield return new Object[]
            {
                new ConnectedUser("loremipsum@gmail.com","1234")
            };
            #endregion

            #region Test with SharerUser
            yield return new Object[]
            {
                new SharerUser("loremipsum@gmail.com","1234")
            };
            #endregion
        }
    }
}
