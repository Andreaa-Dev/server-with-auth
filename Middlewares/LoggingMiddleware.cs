using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    // must have constructor to pass request to next function
    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("logging");
        // Log the incoming request
        _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");

        // measure how long request take
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();

        // Log the outgoing response
        _logger.LogInformation($"Outgoing response: {context.Response.StatusCode} ({stopwatch.ElapsedMilliseconds}ms)");
    }


}