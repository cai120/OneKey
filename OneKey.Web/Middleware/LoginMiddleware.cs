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
        var cultureQuery = context.Request.Query["culture"];
        if (!string.IsNullOrWhiteSpace(cultureQuery))
        {
        }

        // Call the next delegate/middleware in the pipeline.
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