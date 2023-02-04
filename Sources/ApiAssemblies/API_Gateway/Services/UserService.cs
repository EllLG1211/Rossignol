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
        private readonly AppSettings _appSettings;
        private readonly IDataManager _dataManager;

        public UserService(IOptions<AppSettings> appSettings, IDataManager dataManager)
        {
            _dataManager = dataManager;
            _appSettings = appSettings.Value;
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

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Mail),
                    }),
                    Expires = DateTime.UtcNow.AddDays(14),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return user.WithoutPassword();
            }
            catch (ArgumentException e)
            {
                return null;
            }
        }
    }
}
