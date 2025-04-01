using JobSphere.Data;
using JobSphere.ENUMS;
using JobSphere.Mapping;
using JobSphere.Repositories;
using JobSphere.Repositories.Users;
using JobSphere.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


//addinh authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireEmployer", policy => policy.RequireRole(UserRole.Employer.ToString()));
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole(UserRole.Admin.ToString()));
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
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true; // Optional: for better readability
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

}); ;
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
