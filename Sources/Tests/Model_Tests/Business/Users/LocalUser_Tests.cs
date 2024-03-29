﻿using Model.Business.Entries;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace Model_Tests.Business.Users
{
    public class LocalUser_Tests
    {
        /// <summary>
        /// Test if the LocalUser is instanciated.
        /// </summary>
        [Fact]
        public void Constructor_ShouldAssignPassword()
        {
            string password = "1234";
            AbstractUser loUser = new LocalUser(password);
            Assert.Equal(password, loUser.Password);
        }

        /// <summary>
        /// Test if the entries of the user is instanciated.
        /// </summary>
        [Fact]
        public void Constructor_InstantiateUserEntries()
        {
            AbstractUser loUser = new LocalUser("1234");
            Assert.NotNull(loUser.Entries);
        }

        [Fact]
        public void Constructor_ShouldInstantiateEntries()
        {
            AbstractUser user = new LocalUser("1234");
            Assert.NotNull(user.Entries);
        }

        [Fact]
        public void Constructor_ShouldGiveListInstance()
        {
            List<Entry> entries = new List<Entry>();
            Entry entry = new ProprietaryEntry("mail@a.com", "test", "1234", "app");
            entries.Add(entry);
            AbstractUser user = new LocalUser("1234", entries);
            Assert.Contains(entry, user.Entries);
        }

        /// <summary>
        /// Test if add Entry work well
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="entry"></param>
        [Theory]
        [MemberData(nameof(AddEntry_Tests_Data))]
        public void AddEntry_Tests(bool expected, Entry entry)
        {
            AbstractUser loUser = new LocalUser("1234");

            loUser.AddEntry(entry);
            Assert.Equal(expected, loUser.Entries.Contains(entry));
        }

        public static IEnumerable<Object[]> AddEntry_Tests_Data()
        {
            #region Add ProprietaryEntry
            yield return new Object[]
            {
                true,
                new SharedEntry(new ReadOnlyUser("test@test.com", "1234"), "admin","1234","discord")
            };
            #endregion

            #region Add SharedEntry
            yield return new Object[]
            {
                true,
                new ProprietaryEntry("mail@a.com","admin","1234","discord")
            };
            #endregion
        }

        /// <summary>
        /// AddEntry should not add null value.
        /// </summary>
        [Fact]
        public void AddEntry_AddNullShouldNotWork()
        {
            AbstractUser loUser = new LocalUser("1234");
            loUser.AddEntry(null);
            Assert.Empty(loUser.Entries);
        }

        /// <summary>
        /// Test if RemoveEntry work well.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="entry"></param>
        [Theory]
        [MemberData(nameof(RemoveEntry_Tests_Data))]
        public void RemoveEntry_Tests(bool expected, Entry entry)
        {
            AbstractUser loUser = new LocalUser("1234");
            loUser.AddEntry(entry);

            loUser.RemoveEntry(entry);
            Assert.Equal(expected, loUser.Entries.Contains(entry));
        }

        public static IEnumerable<Object[]> RemoveEntry_Tests_Data()
        {
            #region Remove ProprietaryEntry
            yield return new Object[]
            {
                false,
                new SharedEntry(new ReadOnlyUser("test@test.com", "1234"), "admin","1234","discord")
            };
            #endregion

            #region Remove SharedEntry
            yield return new Object[]
            {
                false,
                new ProprietaryEntry("mail@a.com","admin","1234","discord")
            };
            #endregion
        }

        [Theory]
        [MemberData(nameof(Equals_ShouldReturnTrue_Data))]
        public void Equals_ShouldReturnTrue(bool expected, AbstractUser user, object other)
        {
            Assert.True(user.Equals(other) == expected);
        }

        public static IEnumerable<Object[]> Equals_ShouldReturnTrue_Data()
        {
            Guid uid = Guid.NewGuid();
            AbstractUser user = new LocalUser(uid, "1234", null);
            yield return new object[]
            {
                true,
                user,
                user
            };

            yield return new object[]
            {
                false,
                user,
                new LocalUser("12234")
            };

            yield return new object[]
            {
                true,
                user,
                new LocalUser(uid, "12234", null)
            };

            yield return new object[]
            {
                false,
                user,
                new ReadOnlyUser("test", "1234")
            };

            yield return new object[]
            {
                false,
                user,
                "test"
            };

            yield return new object[]
            {
                false,
                user,
                null
            };
        }
    }
}
