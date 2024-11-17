using EcommerceBackend.Data;
using EcommerceBackend.Services;
using EcommerceBackend.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configure DbContext for SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add scoped services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddScoped<ICartService, CartService>(); // Add this line
builder.Services.AddScoped<ICartRepository, CartRepository>(); 

builder.Services.AddEndpointsApiExplorer();


// Configure Authentication with JWT Bearer
var jwtSecret=Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new Exception("JWT_SECRET is not set in .env file");


// Configure Authentication with JWT Bearer
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourapp", // Replace with your app's issuer name
            ValidAudience = "yourapp", // Replace with your app's audience name
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)) // Replace with a secure key
        };
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });


// Build the application
var app = builder.Build();

// Configure middleware pipeline
app.UseAuthentication(); // Enable JWT Authentication
app.UseAuthorization();  // Enable Authorization

// Map controller routes
app.MapControllers();

// Run the application
app.Run();