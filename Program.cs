using Microsoft.Extensions.Logging;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Backend.src.Database;
using Backend.src.Entity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add database service
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Local"));
dataSourceBuilder.MapEnum<Role>();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options
    .UseNpgsql(dataSource);
}
);

var app = builder.Build();

// Test database connection
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    try
    {
        // Attempt to connect to the database and check if we can access the Users table
        dbContext.Database.OpenConnection();  // Open the connection
        dbContext.Database.EnsureCreated();   // Ensures the database is created
        var canConnect = dbContext.Database.CanConnect(); // Tests if the database is reachable

        if (canConnect)
        {
            Console.WriteLine("Database connection successful.");
        }
        else
        {
            Console.WriteLine("Failed to connect to the database.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while trying to connect to the database: {ex.Message}");
    }
    finally
    {
        dbContext.Database.CloseConnection(); // Close the connection
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use the custom logging middleware globally 
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapGet("/", async context =>

{
    await context.Response.WriteAsync("Hello, world! This is a simple request without logging.");
});

app.MapGet("/logger", async context =>

{
    // var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    // // // Log the incoming request
    // logger.LogInformation("Handling request for the root URL '/'");

    // await context.Response.WriteAsync("Hello, world! This is a simple request with logging.");

    // // Log the completion of request handling
    // logger.LogInformation("Finished handling request for the root URL '/'");

    string param = null;
    if (param == null)
    {
        throw new ArgumentNullException(nameof(param), "The parameter cannot be null.");
    }

    await context.Response.WriteAsync("This won't be executed due to the exception.");
});

app.Run();
