public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        // Custom authorization logic
        await _next(context);
    }
}

// Extension method to add the middleware
public static class CustomAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomAuthorization(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomAuthorizationMiddleware>();
    }
}
