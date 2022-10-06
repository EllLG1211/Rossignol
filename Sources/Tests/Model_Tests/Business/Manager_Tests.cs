using Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Model_Tests.Business
{
    public class Manager_Tests
    {
        [Theory]
        [InlineData(true, null, "1234", "1234")]
        [InlineData(true, "test@test.com", null, "1234")]
        [InlineData(true, "test@test.com", "1234", null)]
        [InlineData(true, "", "1234", "1234")]
        [InlineData(true, "test@test.com", "", "1234")]
        [InlineData(true, "test@test.com", "1234", "")]
        [InlineData(false, "test@test.com", "1234", "1234")]
        [InlineData(false, "test@test.com", "1234", "12345")]
        public void Signin_ShouldThrowArgumentNullException(bool expected, string mail, string password, string confirmPassword)
        {
            Manager manager = new Manager();
            if (expected)
            {
                Assert.Throws<ArgumentNullException>(() => { manager.Signin(mail, password, confirmPassword); });
                return;
            } else
            {
                manager.Signin(mail, password, confirmPassword);
                Assert.False(expected);
            }
        }

        [Theory]
        [InlineData(false, null, "1234", "1234")]
        [InlineData(false, "test@test.com", null, "1234")]
        [InlineData(false, "test@test.com", "1234", null)]
        [InlineData(false, "", "1234", "1234")]
        [InlineData(false, "test@test.com", "", "1234")]
        [InlineData(false, "test@test.com", "1234", "")]
        [InlineData(false, "test@test.com", "1234", "1234")]
        [InlineData(true, "test@test.com", "1234", "12345")]
        public void Signin_ShouldThrowArgumentException(bool expected, string mail, string password, string confirmPassword)
        {
            Manager manager = new Manager();
            try
            {
                manager.Signin(mail, password, confirmPassword);
            }
            catch (ArgumentException e)
            {
                Assert.True(expected);
            }
            catch (Exception e)
            {
                Assert.False(expected);
            }
            Assert.False(expected);
        }

        [Fact]
        public void Signin_ShouldInstantiateConnectedUserInManager()
        {
            Manager manager = new Manager();
            manager.Signin("test@test.com", "1234", "1234");
            Assert.NotNull(manager.ConnectedUser);
        }
    }
}
