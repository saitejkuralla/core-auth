using JWTtoken.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JWTtoken.Services
{
    public class AuthenticateService :IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        public AuthenticateService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private List<User> _users = new List<User>()
{
    new User { UserId = 1, FirstName = "Sai", LastName = "Kiran", UserName = "saikiran", Password = "Saikiran", Role = "Admin" },
    new User { UserId = 2, FirstName = "Hari", LastName = "Krishna", UserName = "hari", Password = "Hari", Role = "User" },
   
};


        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault( x =>x.UserName == username && x.Password == password);
            if(user == null)
            {
                return null;
            }
            var tokenhandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokendescriptor);
            user.Token = tokenhandler.WriteToken(token);

            user.Password = null;
            return user;
        }
    }
}
