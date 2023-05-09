# JWT Authentication in ASP.NET Core API

## Introduction
JWT (JSON Web Token) authentication is a method of securing APIs by using tokens encoded as JSON objects. This guide explains how to implement JWT authentication in your ASP.NET Core API.

## Prerequisites
- ASP.NET Core project set up
- Basic understanding of ASP.NET Core and JWT authentication

## Getting Started

### Step 1: Install the Required NuGet Packages
To enable JWT authentication in your ASP.NET Core project, you need to install the following NuGet package:

### Step 2: Configure JWT Authentication in the Startup Class
Open the `Startup.cs` file in your project and make the following modifications:

Add the necessary namespaces at the top of the file:

```csharp
using Microsoft.IdentityModel.Tokens;
using System.Text;
```
In the ConfigureServices method, add the following code to configure JWT authentication:

```csharp
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
In the Configure method, add the following code to enable authentication:
