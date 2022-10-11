using Model.Business.Entries;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using Xunit;

namespace Model_Tests.Business.Users
{
    public class Sharer_Tests
    {
        [Fact]
        public void Constructor_ShouldAssignEmail()
        {
            string mail = "test@test.com";
            SharerUser user = new(mail, "1234");
            Assert.True(mail.Equals(user.Mail));
        }

        [Fact]
        public void Constructor_ShouldAssignPassword()
        {
            string mail = "test@test.com";
            string password = "1234";
            SharerUser user = new(mail, password);
            Assert.True(password.Equals(user.Password));
        }

        [Fact]
        public void Constructor_ShouldInstantiateEntries()
        {
            AbstractUser user = new SharerUser("test@test.com", "1234");
            Assert.NotNull(user.Entries);
        }

        [Fact]
        public void Constructor_ShouldAssignUid()
        {
            Guid uid = Guid.NewGuid();
            List<Entry> entries = new List<Entry>();
            Entry entry = new ProprietaryEntry("test", "1234", "app");
            entries.Add(entry);
            SharerUser user = new(uid, "test@test.com", "1234", entries);
            Assert.True(uid.Equals(user.Uid));
        }

        [Fact]
        public void Constructor_ShouldGiveListInstance()
        {
            List<Entry> entries = new List<Entry>();
            Entry entry = new ProprietaryEntry("test", "1234", "app");
            entries.Add(entry);
            AbstractUser user = new SharerUser("test@test.com", "1234", entries);
            Assert.Contains(entry, user.Entries);
        }

        [Fact]
        public void Constructor_NullEntriesParamater_ShouldInstanciateNewList()
        {
            AbstractUser user = new SharerUser("test@test.com", "1234", null);
            Assert.NotNull(user.Entries);
        }
    }
}
