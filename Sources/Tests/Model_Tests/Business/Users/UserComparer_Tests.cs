using Model.Business.Entries;
using Model.Business.Users;
using Model.Business.Users.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Model_Tests.Business.Users
{
    public class UserComparer_Tests
    {
        private UserComparer _userComparer = new();
        private static Guid _id = Guid.NewGuid();

        [Theory]
        [MemberData(nameof(Equals_TestData))]
        [MemberData(nameof(Equals_TestData_Nullable))]
        public void Equals_Tests(bool expected, AbstractUser? x, AbstractUser? y)
        {
            Assert.Equal(expected, _userComparer.Equals(x, y));
        }

        public static IEnumerable<object?[]> Equals_TestData()
        {
            yield return new object?[]
            {
                true,
                new LocalUser(_id, "userpass", new List<Entry>()),
                new LocalUser(_id, "userpass", new List<Entry>())
            };

            yield return new object?[]
            {
                false,
                new LocalUser(Guid.NewGuid(), "userpass",new List<Entry>()),
                new LocalUser(Guid.NewGuid(), "userpass",new List<Entry>())
            };

            yield return new object?[]
            {
                false,
                new LocalUser(_id, "userpass", new List<Entry>()),
                new LocalUser(_id, "usx", new List<Entry>())
            };
        }

        public static IEnumerable<object?[]> Equals_TestData_Nullable()
        {
            #region Null objects
            yield return new object?[]
            {
                false,
                new LocalUser(Guid.NewGuid(), "userpass",new List<Entry>()),
                null
            };

            yield return new object?[]
            {
                false,
                null,
                new LocalUser(Guid.NewGuid(), "userpass",new List<Entry>())
            };

            yield return new object?[] { true, null, null };
            #endregion
        }
    }
}
