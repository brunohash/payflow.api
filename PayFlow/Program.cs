using System.Text;
using PayFlow.Business;
using Domain;
using Domain.Pagarme;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using PayFlow.Services;
using PayFlow.Services.Caller;
using PayFlow.Services.Caller.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var key = Encoding.ASCII.GetBytes(AuthJwtKey.JwtKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Repository
builder.Services.AddScoped<IAccoutRepository, AccoutRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// Business
builder.Services.AddTransient<AccoutBusiness>();
builder.Services.AddTransient<PaymentBusiness>();
builder.Services.AddTransient<PagarmeBusiness>();

builder.Services.AddTransient<IPagarmeCaller, PagarmeCaller>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// tempo de vida do serviço
builder.Services.AddTransient<TokenService>(); // sempre criar um novo
//builder.Services.AddScoped(); // enquanto a requisição durar
//builder.Services.AddSingleton(); // 1 por aplicação

builder.Services.Configure<PagarmeCredentials>(builder.Configuration.GetSection("Pagarme"));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayFlow", Version = "v1" });

    // Define o esquema de segurança Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira seu token JWT no campo abaixo",
    });

    // Adiciona o esquema de segurança a todas as operações
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = String.Empty;
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PayFlow Payment v1");
    c.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();