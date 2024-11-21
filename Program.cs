using Npgsql;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using System.Text;
using Backend.src.Database;
using Backend.src.Entity;
using Backend.src.Service.Impl;
using Backend.src.Service;
using Backend.src.Abstraction;
using Backend.src.Shared;
using Backend.src.Repository;
using Backend.src.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend API", Version = "v1" });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        }
        );

        options.OperationFilter<CustomAuthOperationFilter>();
    });

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Local"));
dataSourceBuilder.MapEnum<Role>();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(dataSourceBuilder.Build());
}
);


builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services
    .AddScoped<ICategoryService, CategoryService>()
    .AddScoped<IBaseRepo<Category>, CategoryRepo>()

    .AddScoped<IProductService, ProductService>()
    .AddScoped<IBaseRepo<Product>, ProductRepo>()

    .AddScoped<IUserService, UserService>()
    .AddScoped<IUserRepo, UserRepo>()

      .AddScoped<IOrderDetailService, OrderDetailService>()
    .AddScoped<IOrderDetailRepo, OrderDetailRepo>()


    .AddScoped<IOrderService, OrderService>()
    .AddScoped<IOrderRepo, OrderRepo>();


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration["Cors:Origin"]!)
                          .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed((host) => true)
                            .AllowCredentials();
                      });
});


builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/login"; // Endpoint for login
        options.LogoutPath = "/auth/logout"; // Endpoint for logout
        options.AccessDeniedPath = "/auth/accessdenied"; // Endpoint for access denied
        options.Cookie.Name = "AuthCookie";
        options.Cookie.HttpOnly = true; // Prevent access via client-side scripts
        options.ExpireTimeSpan = TimeSpan.FromDays(7); // Cookie expiration
        options.SlidingExpiration = true; // Reset expiration with activity
    });

// Add Authorization
builder.Services.AddAuthorization(
    options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    }
    );

var app = builder.Build();

app.UseRouting();

app.MapGet("/", () => "Server is running");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
