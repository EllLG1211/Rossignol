using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Model;
using System.Diagnostics;

namespace Model_Tests
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
            try
            {
                ProprietaryEntry entry = new(login, password, app);
                Assert.False(throwSuccessExpected);
            } catch 
            {
                Assert.True(throwSuccessExpected);
            }
        }

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
        /// Create data for the <i>Equals</i> test.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Object[]> Equals_test_data()
        {
            yield return new Object[]
            {
                true,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Login", "1234", "Discord")
            }; 
            
            yield return new Object[]
            {
                false,
                new ProprietaryEntry("login", "1234", "Discord"),
                new ProprietaryEntry("Login", "1234", "Discord")
            };

            yield return new Object[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Login", "1235", "Discord")
            };

            yield return new Object[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Login", "1234", "discord")
            };
        }

        /// <summary>
        /// Test the <i>Equals</i> method.
        /// </summary>
        /// <param name="equalsExpected"></param>
        /// <param name="one"></param>
        /// <param name="two"></param>
        [Theory]
        [MemberData(nameof(Equals_test_data))]
        public void Equals_test(bool equalsExpected, Entry one, Entry two)
        {
            Assert.Equal(equalsExpected, ((IEquatable<Entry>)one).Equals(two));
        }

        /// <summary>
        /// Create data for the <i>EqualsUid</i> test.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Object[]> EqualsUid_test_data()
        {
            Entry instance = new ProprietaryEntry("Login", "1234", "Discord");

            yield return new Object[]
            {
                true,
                instance,
                instance
            };

            yield return new Object[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Login", "1234", "Discord")
            };
        }

        /// <summary>
        /// Test the <i>EqualsUid</i> method.
        /// </summary>
        /// <param name="equalsExpected"></param>
        /// <param name="one"></param>
        /// <param name="two"></param>
        [Theory]
        [MemberData(nameof(EqualsUid_test_data))]
        public void EqualsUid_test(bool equalsExpected, Entry one, Entry two)
        {
            Assert.Equal(equalsExpected, one.EqualsUid(two));
        }

        [Fact]
        public void Equals_testWithNull()
        {
            Assert.False(new ProprietaryEntry("Login", "1234", "Discord").Equals(null));
        }

        [Fact]
        public void EqualsUid_testWithNull()
        {
            Assert.False(new ProprietaryEntry("Login", "1234", "Discord").EqualsUid(null));
        }
    }
}
