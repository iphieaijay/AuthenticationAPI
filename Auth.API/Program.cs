using Application.Interfaces;
using Auth.API.Extensions;
using Auth.API.Middleware;
using Auth.Application.Interfaces;
using Auth.Application.Services;
using Auth.Application.Validators;
using Auth.Domain.Contracts;
using Auth.Domain.Entities;
using Auth.Domain.Models;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Encryption;
using Auth.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using UserAuthentication.Infrastructure.Mappings;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, services, configuration) => configuration
      .ReadFrom.Configuration(context.Configuration)
      .Enrich.FromLogContext());

builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddApplicationServices(builder.Configuration);
// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddControllers();

// Bind JwtSettings section to JwtSettings class and register in DI
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


// Register all validators in the current assembly

builder.Services.AddValidatorsFromAssemblyContaining<LoginUserRequestValidator>();
 builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserRequestValidator>();

//builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher<ApplicationUser>, CustomPasswordHasher>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();

// Use Serilog for logging
app.UseSerilogRequestLogging();

app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "User Authentication API v1");
    });
}

app.UseHttpsRedirection();
app.UseExceptionHandler("/error");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
