using System.Reflection;
using InternSotatek.Personal.Application.Usecases.Users.Commands.Create;
using InternSotatek.Personal.Infrastructure;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using InternSotatek.Personal.Application.Usecases.Users.Commands.Update;
using InternSotatek.Personal.Application;
using Serilog;
using Serilog.AspNetCore;
using InternSotatek.Personal.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi  
builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// logger  
Log.Logger = new LoggerConfiguration()
   .WriteTo.Console()
   .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
   .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        }
    );
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
