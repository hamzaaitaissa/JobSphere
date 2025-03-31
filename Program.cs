using JobSphere.Data;
using JobSphere.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<JobSphereContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true; // Optional: for better readability
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

}); ;


var app = builder.Build();


app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
