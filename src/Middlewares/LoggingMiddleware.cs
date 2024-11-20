using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;
    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("logging");
        _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");

        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();

        _logger.LogInformation($"Outgoing response: {context.Response.StatusCode} ({stopwatch.ElapsedMilliseconds}ms)");
    }


}