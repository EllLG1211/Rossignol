using Model.Api.Entities;

namespace API_Gateway.Services
{
    public interface IUserService
    {
        User? Authenticate(string email, string password);
    }
}
