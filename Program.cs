using Npgsql;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using System.Text;
using Swashbuckle.AspNetCore.Filters;

using Backend.src.Database;
using Backend.src.Entity;
using Backend.src.Service.Impl;
using Backend.src.Service;
using Backend.src.Abstraction;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend API", Version = "v1" });
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Bearer token authentication",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "Bearer",
            // it works - remember to add Bearer 
            // Type = SecuritySchemeType.ApiKey,
            // BearerFormat = "JWT"
        }
        );
        // it defines an empty set of scopes
        // This is often used when no specific scopes are required for the security scheme
        // meaning that the API doesn’t enforce any specific permissions or roles.
        // Array.Empty<string>();

        // This applies a filter to all API operations to include the security requirement globally.
        // This means that all endpoints will have the specified security scheme applied to them.
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

// add controllers
builder.Services.AddControllers();

// add database service
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Local"));
dataSourceBuilder.MapEnum<Role>();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options
    .UseNpgsql(dataSourceBuilder.Build());
}
);


// add automapper service
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

// add DI services
builder.Services
    .AddScoped<ICategoryService, CategoryService>()
    .AddScoped<IBaseRepo<Category>, CategoryRepo>()

    .AddScoped<IProductService, ProductService>()
    .AddScoped<IBaseRepo<Product>, ProductRepo>()

    .AddScoped<IUserService, UserService>()
    .AddScoped<IUserRepo, UserRepo>();

// Add Identity services: only the default
// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//     .AddEntityFrameworkStores<DatabaseContext>()
//     .AddDefaultTokenProviders();

// cors
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

// Add JWT Authentication
// by default cookie
builder.Services
.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Add Authorization
builder.Services.AddAuthorization(
    options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    }
    );

var app = builder.Build();

// app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// middleware
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
// cors
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
