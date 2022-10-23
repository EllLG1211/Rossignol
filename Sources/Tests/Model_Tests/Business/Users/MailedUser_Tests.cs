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
        [Fact]
        public void Equals_ShouldReturnFalseIfOtherIsNull()
        {
            MailedUser user = new ReadOnlyUser("test", "1234");
            Assert.False(user.Equals(null));
        }

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
        }
    }
}
