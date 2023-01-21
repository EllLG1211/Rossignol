using API_REST.Controllers.V1;

namespace ApiREST_Tests
{
    [TestClass]
    public class AccountControllerTest
    {
        private readonly AccountsController _accountsController;

        public AccountControllerTest()
        {
            _accountsController = new AccountsController();
        }

        [TestMethod]
        public void GetUserInfo_Test()
        {
            
        }
    }
}