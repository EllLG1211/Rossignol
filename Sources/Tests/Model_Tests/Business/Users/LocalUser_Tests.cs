﻿using Model.Business.Entries;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Model_Tests.Business.Users
{
    public class LocalUser_Tests
    {
        /// <summary>
        /// Test if the LocalUser is instanciated.
        /// </summary>
        [Fact]
        public void Constructor_InstantiateUser()
        {
            AbstractUser loUser = new LocalUser("1234");
            Assert.NotNull(loUser);
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

        [Theory]
        [MemberData(nameof(AddEntry_Tests_Data))]
        public void AddEntry_Tests(bool expected, Entry entry)
        {
            AbstractUser loUser = new LocalUser("1234");

            loUser.AddEntry(entry);
            Assert.Equal(expected,loUser.Entries.Contains(entry));
        }

        public static IEnumerable<Object[]> AddEntry_Tests_Data()
        {
            #region Add ProprietaryEntry
            yield return new Object[]
            {
                true,
                new ProprietaryEntry("admin","1234","discord")
            };
            #endregion

            #region Add SharedEntry
            yield return new Object[]
            {
                true,
                new SharedEntry("admin","1234","discord")
            };
            #endregion
        }

        [Fact]
        public void AddEntry_AddNullShouldNotWork()
        {
            AbstractUser loUser = new LocalUser("1234");
            loUser.AddEntry(null);
            Assert.Empty(loUser.Entries);
        }

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
                new ProprietaryEntry("admin","1234","discord")
            };
            #endregion

            #region Remove SharedEntry
            yield return new Object[]
            {
                false,
                new SharedEntry("admin","1234","discord")
            };
            #endregion
        }
    }
}
