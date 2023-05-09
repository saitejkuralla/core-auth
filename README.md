# JWT Authentication in ASP.NET Core API

## Introduction
JWT (JSON Web Token) authentication is a method of securing APIs by using tokens encoded as JSON objects. This guide explains how to implement JWT authentication in your ASP.NET Core API.

## Prerequisites
- ASP.NET Core project set up
- Basic understanding of ASP.NET Core and JWT authentication

## Getting Started

### Step 1: Install the Required NuGet Packages
To enable JWT authentication in your ASP.NET Core project, you need to install the following NuGet package:
```
dotnet add package System.IdentityModel.Tokens.Jwt
```

### Step 2: Configure JWT Authentication in the Startup Class
Open the `Startup.cs` file in your project and make the following modifications:

Add the necessary namespaces at the top of the file:

```
using Microsoft.IdentityModel.Tokens;
using System.Text;
```
In the ConfigureServices method, add the following code to configure JWT authentication:

```
var appSettingsSection = Configuration.GetSection("AppSettings");
services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Key);

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

```
🔍 Retrieving Configuration Settings:

- we have to Retrieve the configuration section named "AppSettings" from the configuration file.
- after that we need to Bind the configuration section to the AppSettings class and then Retrieve the strongly-typed AppSettings object from the configuration.
- Next,Convert the JWT key to a byte array using ASCII encoding.
- Configuring Authentication Services:

🛠️ Configure the authentication services in the application.
- First we need to Set the default authentication and challenge schemes to JWT Bearer authentication.
- Add the JWT Bearer authentication scheme and configure its options.
- Specify that the JWT token should be saved in the authentication properties.
- Set the token validation parameters for JWT validation.
✅ Enable validation of the issuer signing key.
- Set the issuer signing key for JWT token signature validation.
❌ Disable validation of the issuer.
❌ Disable validation of the audience.
By following these steps, you can configure JWT authentication in your ASP.NET Core API.

### Step 3: Create the User Model
Create a new class file named User.cs and add the following code to define the User model:
```
namespace JWTtoken.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
```
### Step 4: Create the Authentication Controller
Create a new controller file named AuthenticationController.cs and add the following code to define the authentication controller:

```
using JWTtoken.Models;
using JWTtoken.Services;
using Microsoft.AspNetCore.Mvc;

namespace JWTtoken.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
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
```
- The AuthenticationController is responsible for handling authentication-related operations.
- It is a part of the JWTtoken project.
- The controller is defined with the route [controller], where [controller] is replaced with the actual name "Authentication".
- It is an API controller that inherits from ControllerBase.
- The controller has a constructor that injects an IAuthenticateService instance for user authentication.
- The Post action method handles the POST request for user authentication.
- It takes a User object containing the user credentials (username and password) as the request body.
- The Authenticate method of the injected service is called to validate the user credentials.
- If the user is not found or the password is incorrect, a BadRequest response is returned with an appropriate error message.
- If the user is successfully authenticated, an Ok response is returned with the user object, which includes the JWT token.
- The AuthenticationController is an essential component in the JWT token-based authentication flow.
- By following these steps, the AuthenticationController facilitates user authentication by verifying the credentials and returning the appropriate responses.

### Step 5: Update the ConfigureServices Method
In the ConfigureServices method of the Startup.cs file, add the following code to register the services:
```
services.AddScoped<IAuthenticateService, AuthenticateService>();
```
### Step 6: Protect API Endpoints with JWT Authentication

- Use the [Authorize(Roles = "User,Admin")] attribute on the Get action method to specify that only authenticated users with the roles "User" or "Admin" can access the endpoint:
```
[HttpGet]
[Authorize(Roles = "User,Admin")]
public IEnumerable<WeatherForecast> Get()
{
  
}
```
- By adding the [Authorize(Roles = "User,Admin")] attribute to the Get action method, you ensure that only users with the roles "User" or "Admin" can access the endpoint.
-  Similarly, the [Authorize(Roles = "Admin")] attribute on the GetForAdmin action method restricts access to users with the "Admin" role.
### Step 7: Configure AppSettings
- In the appsettings.json file, configure the AppSettings section with the JWT key:
```
{
  "AppSettings": {
    "Key": "YourJWTKeyHere"
  },
  ```
## Conclusion
By following these steps, We can implement JWT authentication in your ASP.NET Core API. JWT authentication provides a secure way to protect your API endpoints and restrict access to authorized users based on their roles.
