JWT Token Authentication Sample Application

This is a sample ASP.NET Core application that demonstrates how to implement JWT (JSON Web Token) token authentication. The application provides an API for generating and validating JWT tokens, and also includes an example controller that requires authentication and authorization using JWT tokens.

--------

The application provides the following API endpoints:

POST /Authentication: This endpoint is used to authenticate a user and generate a JWT token. It expects a JSON payload containing the UserName and Password properties. If the authentication is successful, it returns an Ok response with the generated token. If the authentication fails, it returns a BadRequest response with an error message.

GET /WeatherForecast: This endpoint is an example protected route that requires authentication. It returns a list of weather forecasts. Only users with the roles "User" or "Admin" can access this endpoint. The token should be included in the request headers using the Authorization header with the value Bearer <token>.

GET /WeatherForecast/admin: This endpoint is another example protected route that requires authentication and the "Admin" role. Only users with the "Admin" role can access this endpoint. It returns a simple message indicating that the method is only accessible by admins.

----------

The application defines two authorization policies:

UserAndAdmin: This policy requires the user to have either the "User" or "Admin" role.

AdminOnly: This policy requires the user to have the "Admin" role.

These policies are used to restrict access to the protected endpoints in the WeatherForecastController.

------------
The AuthenticateService class is responsible for authenticating user credentials and generating JWT tokens.

The Startup.cs file contains the configuration for JWT authentication and authorization.

The AppSettings.cs file defines the model for the AppSettings configuration, including the JWT key.
