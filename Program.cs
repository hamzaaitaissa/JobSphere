using JobSphere.Data;
using JobSphere.ENUMS;
using JobSphere.Mapping;
using JobSphere.Middleware;
using JobSphere.Policies.Handlers;
using JobSphere.Policies.Requirements;
using JobSphere.Repositories;
using JobSphere.Repositories.Jobs;
using JobSphere.Repositories.Users;
using JobSphere.Services;
using JobSphere.Services.Jobs;
using JobSphere.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
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
Debug.WriteLine(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Debug.WriteLine("AUTH FAILED: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Debug.WriteLine("TOKEN VALIDATED");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Debug.WriteLine("JWT CHALLENGE: " + context.ErrorDescription);
            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
             // **READ FROM SAME PATH AS LOGIN**
             builder.Configuration.GetSection("JwtSettings:SecretKey").Value
         )), 

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value, // **READ FROM SAME PATH**

        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("JwtSettings:Audience").Value, // **READ FROM SAME PATH**

        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5)
      
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
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IPasswordHasherService, Pbkdf2PasswordHasherService>();


var app = builder.Build();



app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
