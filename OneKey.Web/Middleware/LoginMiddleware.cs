using Microsoft.AspNetCore.Authentication;

namespace OneKey.Web.Middleware;

public class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            await context.SignOutAsync();
        }
        await _next(context);
    }
}

public static class RequestLoginMiddlewareExtensions
{
    public static IApplicationBuilder UseLoginMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoginMiddleware>();
    }
}