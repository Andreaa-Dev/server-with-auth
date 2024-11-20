using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Backend.src.Middlewares
{

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "An internal server error occurred.",
                    Detailed = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}