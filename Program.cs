using JobSphere.Data;
using JobSphere.ENUMS;
using JobSphere.Mapping;
using JobSphere.Middleware;
using JobSphere.Policies.Handlers;
using JobSphere.Policies.Requirements;
using JobSphere.Repositories;
using JobSphere.Repositories.Users;
using JobSphere.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


//addinh authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOwnershipPolicy", policy =>
       policy.Requirements.Add(new UserOwnershipRequirement()));
});
//jwt authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
        ValidAudience = jwtSettings.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});


builder.Services.AddDbContext<JobSphereContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register HttpContextAccessor (needed for the modified UserOwnershipHandler)
builder.Services.AddHttpContextAccessor();

// Registerin Handlers as Scoped
builder.Services.AddSingleton<IAuthorizationHandler, UserOwnershipHandler>();
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>(); // LIKELY NEEDS TO BE SCOPED

// Registering AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Registering Controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true; // Optional: for better readability
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Registering Repositories and Services as Scoped
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
