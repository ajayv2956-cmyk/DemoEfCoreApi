using DemoEfCoreApi.Data;
using DemoEfCoreApi.Helpers;
using DemoEfCoreApi.Services;
using DemoEfCoreApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DemoEfCoreApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // DbContext
            services.AddDbContext<DemoEfDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // JWT settings
            var jwtSection = config.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSection);
            var jwtSettings = jwtSection.Get<JwtSettings>();
            var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

            // JWT authentication
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = "JwtBearer";
                opt.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            // DI registration
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
