using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PensionContManageSystem.Domain.Interfaces;
using PensionContManageSystem.Infrastructure;
using PensionContManageSystem.Infrastructure.Repository;
using PensionContManageSystem.Services;
using PensionContManageSystem.Utility;
using Serilog;
using Serilog.Events;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var DbConnection = builder.Configuration.GetConnectionString("ConnectStr");

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(DbConnection));
builder.Services.AddScoped<IUniOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(Mapping));

builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("ConnectStr")));
builder.Services.AddHangfireServer();

//Add Caching
builder.Services.AddResponseCaching();//inline cache

builder.Services.ConfigureHttpCacheHeaders();//This initialize the two service calls in d ServiceExtension class

//Logger configuration
Log.Logger = new LoggerConfiguration().WriteTo.File(path: "c:\\pensionContManagementSystem\\logs\\log-.txt",
    outputTemplate: "{Timestamp:yyyy-mm-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:1j}{NewLine}{Exception}",
    rollingInterval: RollingInterval.Day,
    restrictedToMinimumLevel: LogEventLevel.Information
    ).CreateLogger();
builder.Host.UseSerilog();



//Here I configure the global Cache(Caching)
builder.Services.AddControllers(config =>
{
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
    {
        Duration = 120
    });
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//Add Swagger Documentation
builder.Services.AddSwaggerGen(u => u.SwaggerDoc("v1",
    new OpenApiInfo()
    {
        Title = "Pension Contribution System",
        Version = "v1",
        //Description = "This is the description of the Pension Contribution Management System",
        Description = "This Robust Pension Contribution System web API was built using the latest version of ASP.NET Core version 9.\r\n\r\n Repository Pattern and solid principle were properly observed.\r\n\r\n with Entity Framework Core and Clean Architectural Design Patterns.\r\n\r\n which allows Customer Onboarding, Contribution Processing\r\n\r\n (One per Month but multiple contributions for Voluntary) \r\n\r\n and Calculation of the Total Contributions made for certain duration.)\r\n\r\n The following features were implemented:\r\n\r\n Logging Events & Errors, Global Error Handling,\r\n\r\n API Documentation using SwaggerUI,\r\n\r\n Inline and Global Caching for the GET APIs, Unit Of Work, Generic Repository Pattern and lots more.\r\n\r\n CHECK MY PORTFOLIO VIA THE LINK BELOW\r\n\r\n Technologies Used: C#, EFCore, ASP.NETCore, MSSQL Server, SwaggerUI(for Testing and Documentation), Postman and GitHub.",

        Contact = new OpenApiContact()
        {
            Email = "olaoluwaesan.dev@gmail.com",
            Name = "Check MY Portfolio",
            Url = new Uri("https://olasquare202.github.io/Boostrap-V5-Project-with-SASS/")

        }
    }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.ConfigureGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseResponseCaching();//Register the middleware for Cache

app.UseHttpCacheHeaders();//Register the middleware for marvin.Cache.Hearder for better performance 

app.UseAuthorization();

app.MapControllers();

ApplyMigration();//I invoked d Mtd here in d pipeline

app.Run();

void ApplyMigration()//Configure auto Update-database here
{
    using (var scope = app.Services.CreateScope())//Get all d services registered in d application
                                                  //but we only need d DbContext service
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();//Dis is d only service we required here
        if (_db.Database.GetPendingMigrations().Count() > 0)//Dis check if Count of PendingMigration is > 0 it means dat there is migration that have not been applied to DB
        {
            _db.Database.Migrate();//then Dis line apply it(i.e update-database)
        }
    }
}
