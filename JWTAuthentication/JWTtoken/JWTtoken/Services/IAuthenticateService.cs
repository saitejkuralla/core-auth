using JWTtoken.Models;

namespace JWTtoken.Services
{
    public interface IAuthenticateService
    {
        User Authenticate(string username, string password);
    }
}
