using JWTtoken.Models;
using JWTtoken.Services;
using Microsoft.AspNetCore.Mvc;

namespace JWTtoken.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private IAuthenticateService _authenticateService;

        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        // POST: /Authentication
        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            // Call the Authenticate method of the injected service to validate the user credentials
            var user = _authenticateService.Authenticate(model.UserName, model.Password);

            // If the user is not found or the password is incorrect, return a BadRequest response
            if (user == null)
            {
                return BadRequest(new { message = "Username or Password is Incorrect" });
            }

            // If the user is successfully authenticated, return an Ok response with the user object
            return Ok(user);
        }
    }
}
