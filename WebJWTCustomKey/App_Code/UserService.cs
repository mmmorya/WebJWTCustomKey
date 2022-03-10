using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebJWTCustomKey.Models;

namespace WebJWTCustomKey.App_Code
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        JUser GetAll();
        
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            
            // return null if user not found
            if (model == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(model.UserID.ToString(),model.UserToken);

            return new AuthenticateResponse(new JUser { }, token);
        }

        public JUser GetAll()
        {
            var j = new JUser
            {
                Id = 1,
                UserToken = "tsdfasdfasdf"
            };
            return j;
        }

        

        // helper methods

        private string generateJwtToken(string id,string Token)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("fcf7325e39734ffba2fea2bb9a741cc9");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", id) }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
