using HUE5_Dorian.Middleware;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using System.Text;
using System;

namespace HUE5_Dorian.Middleware
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string AuthHeader = "Authorization";

        public BasicAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey(AuthHeader))
            {
                await _next(context);
                return;
            }

            var authHeader = context.Request.Headers[AuthHeader].ToString();
            if (authHeader != null && authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Basic ".Length).Trim();
                var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = credentialString.Split(':');

                if (credentials.Length == 2 && ValidateCredentials(credentials[0], credentials[1]))
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, credentials[0]), new Claim(ClaimTypes.Role, "Admin") };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    context.User = new ClaimsPrincipal(identity);
                }
            }

            await _next(context);
        }

        private bool ValidateCredentials(string username, string password)
        {
            // Hardcoded credentials
            return username == "admin" && password == "Admin@1234";
        }
    }
}
