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
        [Theory]
        [InlineData("schtroumpf", "truc", "abracadabra", true)]
        [InlineData("lorem.ipsum@mail.com", "Lorem Ipsum", "abracadabra", true)]
        [InlineData("schtroumpf", "truc", null, false)]
        [InlineData("korè@bidule", "Wikipédia", "abracadabra", true)]
        public void Constructor_ShouldAssignValues(string login, string website, string note, bool noteSuccessExpected)
        {
            ProprietaryEntry entry = new(42, login, "lorem ipsum", website, note);
            Assert.Equal(login, entry.Login);
            Assert.Equal(website, entry.Website);
            if (noteSuccessExpected) Assert.Equal(note, entry.Note);
            else Assert.NotEqual(note, entry.Note);
        }

        [Fact]
        public void Constructor_ShouldReassignNote()
        {
            ProprietaryEntry entry = new(42, "loremipsum@gmail.com", "rickroll", "Discord");
            Assert.Equal(string.Empty, entry.Note);
        }

        [Fact]
        public void Label_ShouldReturnLoginAndWebsite()
        {
            ProprietaryEntry entry = new(42, "loremipsum@gmail.com", "rickroll", "Discord");
            Assert.Equal("Discord - loremipsum@gmail.com", entry.Label);
        }
    }
}
