using ApiEntities;

namespace API_REST.Services
{
    public interface IAccountServices
    {
        public UserEntity? GetUser(string id);

        public UserEntity? GetUserByEmail(string email);

        public bool AddUser(AccountEntity user);
        bool DeleteUser(string id);
        bool UpdateUser(string id, AccountEntity account);
    }
}
