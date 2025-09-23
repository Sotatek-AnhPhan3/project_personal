using System.Reflection;
using InternSotatek.Personal.Application.Users.UseCases.Commands.Create;
using InternSotatek.Personal.Infrastructure;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// connection string
builder.Services.AddDbContext<PersonalDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDbContext"));
});

builder.Services.AddMediatR(cfg =>
		cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly)
);
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
