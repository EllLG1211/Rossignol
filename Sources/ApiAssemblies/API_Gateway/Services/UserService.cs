using API_Gateway.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.Api.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Model.Business;
using Model.Business.Users;

namespace API_Gateway.Services
{
    public class UserService : IUserService
    {
        private readonly IDataManager _dataManager;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IDataManager dataManager, IJwtUtils jwtUtils)
        {
            _dataManager = dataManager;
            _jwtUtils= jwtUtils;
        }

        public User? Authenticate(string email, string password)
        {
            try
            {
                var userEntity = _dataManager.GetUser(email, password) as ConnectedUser;

                if (userEntity == null)
                {
                    return null;
                }

                User user = new User(userEntity.Uid, userEntity.Mail);


                user.Token = _jwtUtils.GenerateJwtToken(user);

                return user.WithoutPassword();
            }
            catch (ArgumentException e)
            {
                return null;
            }
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException("We can't get all users in IDataManager");
        }

        public User GetById(int id)
        {
            throw new NotImplementedException("We can't get a user by his id in IDataManager");
        }
    }
}
