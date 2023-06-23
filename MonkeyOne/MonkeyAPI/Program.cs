using MonkeyAPI.Models;
using Npgsql;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.Configuration;



var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// Get configuration
IConfiguration configuration = builder.Configuration;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console() // Writing to Console 
    .WriteTo.PostgreSQL(configuration.GetConnectionString("MonkeyDB"),"\"Logs\"")
    .CreateLogger();

builder.Host.UseSerilog();

try
{

builder.Services.AddDbContext<MonkeyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MonkeyDB")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        builder.WithOrigins(
                            "http://localhost:4200"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
