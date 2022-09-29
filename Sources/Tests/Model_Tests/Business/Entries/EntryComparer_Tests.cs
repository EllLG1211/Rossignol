using Model.Business.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Model_Tests.Business.Entries
{
    public class EntryComparer_Tests
    {
        private EntryComparer _entryComparer = new();

        [Theory]
        [MemberData(nameof(Equals_TestData))]
        [MemberData(nameof(Equals_TestData_Nullable))]
        public void Equals_Tests(bool expected, Entry? x, Entry? y)
        {
            Assert.Equal(expected, _entryComparer.Equals(x, y));
        }

        public static IEnumerable<object?[]> Equals_TestData()
        {
            #region Identical values
            yield return new object?[]
            {
                true,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Login", "1234", "Discord")
            };

            yield return new object?[]
            {
                true,
                new SharedEntry("Login", "1234", "Discord"),
                new SharedEntry("Login", "1234", "Discord")
            };
            #endregion

            #region Radically different values
            yield return new object?[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("DefinitelyNot", "1234", "Discord")
            };

            yield return new object?[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Login", "5678", "Discord")
            };

            yield return new object?[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Login", "1234", "Reddit")
            };
            #endregion

            #region Case sensitivity
            yield return new object?[]
            {
                false,
                new ProprietaryEntry("login", "1234", "Discord"),
                new ProprietaryEntry("Login", "1234", "Discord")
            };

            yield return new object?[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Login", "1234", "discord")
            };
            #endregion

            #region Same values but swapped around
            yield return new object[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Discord", "1234", "Login")
            };

            yield return new object[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Discord", "Login", "1234")
            };

            yield return new object[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("1234", "Login", "Discord")
            };

            yield return new object[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Login", "Discord", "1234")
            };
            #endregion

            #region Objects of different types
            yield return new object?[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                new SharedEntry("Login", "1234", "Discord")
            };
            #endregion
        }

        public static IEnumerable<object?[]> Equals_TestData_Nullable()
        {
            #region Null objects
            yield return new object?[]
            {
                false,
                new ProprietaryEntry("Login", "1234", "Discord"),
                null
            };

            yield return new object?[]
            {
                false,
                null,
                new ProprietaryEntry("Login", "1234", "Discord")
            };

            yield return new object?[] { true, null, null };
            #endregion
        }

        [Theory]
        [MemberData(nameof(GetHashCode_TestData))]
        public void GetHashCode_Tests(Entry x, Entry y)
        {
            bool equals = _entryComparer.Equals(x, y);
            bool hashEquals = _entryComparer.GetHashCode(x) == _entryComparer.GetHashCode(y);
            Assert.Equal(equals, hashEquals);
        }

        public static IEnumerable<Entry[]> GetHashCode_TestData()
        {
            #region Identical values
            yield return new Entry[] {
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Login", "1234", "Discord")
            };

            yield return new Entry[] {
                new SharedEntry("Login", "1234", "Discord"),
                new SharedEntry("Login", "1234", "Discord")
            };
            #endregion

            #region Different values
            yield return new Entry[] {
                new ProprietaryEntry("Login", "1234", "Discord"),
                new ProprietaryEntry("Login", "5869", "Reddit")
            };
            #endregion

            #region Different types but same values
            yield return new Entry[] {
                new ProprietaryEntry("Login", "1234", "Discord"),
                new SharedEntry("Login", "1234", "Discord")
            };
            #endregion
        }
    }
}