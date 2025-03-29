using JobSphere.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<JobSphereContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true; // Optional: for better readability
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

}); ;
app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
