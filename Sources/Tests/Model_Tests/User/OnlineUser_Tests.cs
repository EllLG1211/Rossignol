using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Model.User;

namespace Model_Tests.User
{
    public class OnlineUser_Tests
    {
        /// <summary>
        /// Test if class constructor set the correct values.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="app"></param>
        /// <param name="note"></param>
        /// <param name="noteSuccessExpected"></param>
        [Theory]
        [InlineData(false, "sarmat@torba.com")]
        [InlineData(false, "kon@foxmail.cn")]
        [InlineData(true, "pete@totallyreal")]
        [InlineData(true, "zabuk@machina/../../index.php")]
        [InlineData(true, "grineerzrodak.skp")]
        public void Constructor_ShouldAssignValuesOrThrow(bool shouldThrow,string email)
        {
            try
            {
            AbstractUser user = new OnlineUser(email);
            Assert.Equal(email, user.email.Address);

            }
            catch
            {
                Assert.True(shouldThrow);
            }
        }
    }
}