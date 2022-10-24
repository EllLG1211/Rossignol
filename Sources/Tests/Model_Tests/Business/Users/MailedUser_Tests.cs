using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Model_Tests.Business.Users
{
    public class MailedUser_Tests
    {
        [Theory]
        [MemberData(nameof(Equals_ShouldReturnTrue_Data))]
        public void Equals_ShouldReturnTrue(bool expected, MailedUser user, object other)
        {
            Assert.True(user.Equals(other) == expected);
        }

        public static IEnumerable<Object[]> Equals_ShouldReturnTrue_Data()
        {
            MailedUser user = new ConnectedUser("test", "1234");

            yield return new object[]
            {
                true,
                user,
                user
            };

            yield return new object[]
            {
                true,
                user,
                new ConnectedUser("test", "12234")
            };

            yield return new object[]
            {
                false,
                user,
                new ConnectedUser("tedst", "12234")
            };

            yield return new object[]
            {
                false,
                user,
                new LocalUser("1234")
            };

            yield return new object[]
            {
                true,
                user,
                new ReadOnlyUser("test", "1234")
            };

            yield return new object[]
            {
                false,
                user,
                null
            };

            yield return new object[]
            {
                false,
                user,
                "test"
            };
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferentAccordingInstance()
        {
            AbstractUser user = new ConnectedUser("test", "1234");
            AbstractUser user2 = new ConnectedUser("test", "1234");
            Assert.NotEqual(user.GetHashCode(), user2.GetHashCode());
        }
    }
}
