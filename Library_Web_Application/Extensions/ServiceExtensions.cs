using System.Text;
using Contracts;
using Entities;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;

namespace Library_Web_Application.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    
    public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddDbContext<ApplicationContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("sqlConnection")));
    
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentityCore<User>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 10;
            o.User.RequireUniqueEmail = true;
        });
        builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),
            builder.Services);
        builder.AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration
        configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetSection("validIssuer").Value;
        services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new
                        SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });
    }
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1"
            });
        s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Place to add JWT with Bearer",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        s.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Name = "Bearer",
                },
                new List<string>()
            }
        });
        });
    }
    
    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();
}
