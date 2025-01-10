using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _username;
    private readonly string _password;

    public BasicAuthMiddleware(RequestDelegate next, string username, string password)
    {
        _next = next;
        _username = username;
        _password = password;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = 401;
                return;
            }

            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            var username = credentials[0];
            var password = credentials[1];

            if (username != _username || password != _password)
            {
                context.Response.StatusCode = 401;
                return;
            }
        }

        await _next(context);
    }
}



