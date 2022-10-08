using Model;
using Model.Business.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Model_Tests.Business.Entries
{
    public class SharedEntry_Tests
    {
        /// <summary>
        /// Test if class constructor set the correct values.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="app"></param>
        /// <param name="note"></param>
        /// <param name="noteSuccessExpected"></param>
        [Theory]
        [InlineData("schtroumpf", "truc", "abracadabra", true)]
        [InlineData("lorem.ipsum@mail.com", "Lorem Ipsum", "abracadabra", true)]
        [InlineData("schtroumpf", "truc", null, false)]
        [InlineData("korè@bidule", "Wikipédia", "abracadabra", true)]
        public void Constructor_ShouldAssignValues(string login, string app, string note, bool noteSuccessExpected)
        {
            SharedEntry entry = new(login, "lorem ipsum", app, note);
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
        [InlineData(null, "Lorem ipsum", "abracadabra", true)]
        [InlineData("schtroumpf", null, "abracadabra", true)]
        [InlineData("schtroumpf", "Lorem ipsum", null, true)]
        [InlineData(null, null, null, true)]
        [InlineData("schtroumpf", "Lorem ipsum", "Avadra kevadra", false)]
        public void Constructor_ShouldThrowArgumentNullException(string login, string password, string app, bool throwSuccessExpected)
        {
            try
            {
                SharedEntry entry = new(login, password, app);
                Assert.False(throwSuccessExpected);
            }
            catch
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
            SharedEntry entry = new("loremipsum@gmail.com", "rickroll", "Discord");
            Assert.Equal(string.Empty, entry.Note);
        }

        #region Test constructor with Guid
        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Constructor_Guid_ShouldThrowArgumentNullException(bool throwSuccessExpected, bool useGuid)
        {
            if (throwSuccessExpected)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    if (useGuid)
                    {
                        SharedEntry entry = new(Guid.NewGuid(), "login", "1234", "app");
                    }
                    else
                    {
                        SharedEntry entry = new(null, "login", "1234", "app");
                    }
                });
                return;
            }
            Assert.False(throwSuccessExpected);
        }
        #endregion
    }
}
