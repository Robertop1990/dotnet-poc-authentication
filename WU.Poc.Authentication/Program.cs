using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WU.Poc.Authentication.Configs;
using WU.Poc.Authentication.Domain;
using WU.Poc.Authentication.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = builder.Configuration.GetSection("Authentication").Get<AuthenticationSettings>()
    ?? throw new InvalidOperationException("AuthenticationSettings no esta configurado");

// Add services to the container.

builder.Services.AddDbContext<WUDbContext>(options =>
    options.UseInMemoryDatabase("BDInMemory"));

//Configurar autenticación con JWT
var key = Encoding.ASCII.GetBytes(authenticationSettings.JwtKey);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WUDbContext>();

    context.ApiKeys.AddRange(
        new ApiKey { Id = 1, Key = "12345678", IsActive = true },
        new ApiKey { Id = 2, Key = "123456789", IsActive = false }
    );
    context.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
