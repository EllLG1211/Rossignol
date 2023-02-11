using API_Gateway.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.Api.Entities;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Gateway.Helpers
{


    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public string? ValidateJwtToken(string token);
        public string? ExtractToken(string authorizationHeader);
    }

    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<JwtUtils> _logger;

        public JwtUtils(IOptions<AppSettings> appSettings, ILogger<JwtUtils> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Mail),
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            _logger.LogInformation($"{nameof(JwtUtils)}: token not null");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                _logger.LogInformation($"{nameof(JwtUtils)}: token validated");


                var jwtToken = (JwtSecurityToken)validatedToken;

                _logger.LogInformation($"{nameof(JwtUtils)}: cast succeeded");


                var email = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value;

                _logger.LogInformation($"{nameof(JwtUtils)}: email extracted");

                // return user id from JWT token if validation successful
                return email;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public string? ExtractToken(string authorizationHeader)
        {
            var parts = authorizationHeader.Split(" ");
            if (parts.Length == 2 && parts[0] == "Bearer")
            {
                return parts[1];
            }
            else return null;
        }
    }
}
