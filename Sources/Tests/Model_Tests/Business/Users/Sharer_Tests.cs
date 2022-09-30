using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Model_Tests.Business.Users
{
    public class Sharer_Tests
    {
        [Theory]
        [InlineData(false, "sarmat@torba.com")]
        [InlineData(false, "kon@foxmail.cn")]
        [InlineData(true, "pete@totallyreal")]
        [InlineData(true, "zabuk@machina/../../index.php")]
        [InlineData(true, "grineerzrodak.skp")]
        public void Constructor_ShouldAssignValuesOrThrow(bool shouldThrow, string email)
        {
            try
            {
                Sharer user = new Sharer(email);
                Assert.Equal(email, user.email.Address);

            }
            catch
            {
                Assert.True(shouldThrow);
            }
        }
    }
}
