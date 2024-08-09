using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Access the logger
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use the custom logging middleware
app.UseMiddleware<LoggingMiddleware>();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello, world! This is a simple request without logging.");
});

app.MapGet("/logger", async context =>
{
    // Log the incoming request
    logger.LogInformation("Handling request for the root URL '/'");

    await context.Response.WriteAsync("Hello, world! This is a simple request with logging.");

    // Log the completion of request handling
    logger.LogInformation("Finished handling request for the root URL '/'");
});

app.Run();
