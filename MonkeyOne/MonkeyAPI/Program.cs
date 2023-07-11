using MonkeyAPI.Models;
using Npgsql;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using Microsoft.Extensions.Configuration;
using Serilog.Ui.PostgreSqlProvider;
using Serilog.Ui;
using Serilog.Ui.Web;
using Microsoft.AspNetCore.Routing;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// Add SerilogUi to Configuration Services
IServiceCollection services = builder.Services;

// Get configuration
IConfiguration configuration = builder.Configuration;

builder.Services.AddMonkeyAPIServices();
// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console() // Writing to Console 
    //.WriteTo.PostgreSQL(configuration.GetConnectionString("MonkeyDB"),"logs")
    .CreateLogger();

builder.Host.UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(configuration));

try
{

builder.Services.AddSerilogUi(options => options.UseNpgSql(builder.Configuration.GetConnectionString("MonkeyDB"), "logs"));
builder.Services.AddSerilogUi(options => options.UseNpgSql(builder.Configuration.GetConnectionString("MonkeyDB"), "\"Logs\""));    

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
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true); // allow any origin

});
});

    
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    

    }

    app.UseCors("AllowAngularOrigins");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true)); 
       

    app.UseSerilogUi();

    app.UseEndpoints(endpoints =>
    {
        app.MapControllers();
    });
        


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
