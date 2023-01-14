using ApiEntities;

namespace API_REST.Services
{
    public interface IAccountServices
    {
        public UserEntity? GetUser(string id);
    }
}
