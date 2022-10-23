using Data;
using Model.Business;
using Model.Business.Users;

namespace Data_Tests
{
    public class Stub_Tests
    {
        [Fact]
        public void Constructor_ShouldAddItemsInTheList()
        {
            IDataManager stub = new Stub();
            Assert.NotNull(stub.GetUser("test@test.com","1234"));
        }

        [Fact]
        public void GetEntries_ShouldReturnEntry()
        {
            IDataManager stub = new Stub();
            AbstractUser user = stub.GetUser("test@test.com", "1234");
            Assert.NotEmpty(stub.GetEntries(user));
        }

        [Fact]
        public void GetUser_ShouldInstantiateConnectedUser()
        {
            IDataManager stub = new Stub();
            Assert.IsType<ConnectedUser>(stub.GetUser("test@test.com", "1234"));
        }

        [Fact]
        public void Register_ShouldAddUserToUserList()
        {
            string mail = "test";
            string password = "1234";
            IDataManager stub = new Stub();
            AbstractUser user = new ConnectedUser(mail, password);
            stub.Register(user);
            Assert.NotNull(stub.GetUser(mail, password));
        }
    }
}