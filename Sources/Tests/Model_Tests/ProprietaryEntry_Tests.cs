using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Model;

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
        [InlineData("schtroumpf", "truc", "abracadabra", true)]
        [InlineData("lorem.ipsum@mail.com", "Lorem Ipsum", "abracadabra", true)]
        [InlineData("schtroumpf", "truc", null, false)]
        [InlineData("korè@bidule", "Wikipédia", "abracadabra", true)]
        public void Constructor_ShouldAssignValues(string login, string app, string note, bool noteSuccessExpected)
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
        [InlineData(null, "Lorem ipsum", "abracadabra", true)]
        [InlineData("schtroumpf", null, "abracadabra", true)]
        [InlineData("schtroumpf", "Lorem ipsum", null, true)]
        [InlineData("schtroumpf", "Lorem ipsum", "Avadra kevadra", false)]
        public void Constructor_ShouldThrowArgumentNullException(string login, string password, string app, bool throwSuccessExpected)
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
        /// Test the label getter. 
        /// </summary>
        [Fact]
        public void Label_ShouldReturnLoginAndWebsite()
        {
            ProprietaryEntry entry = new("loremipsum@gmail.com", "rickroll", "Discord");
            Assert.Equal("Discord - loremipsum@gmail.com", entry.Label);
        }
    }
}
