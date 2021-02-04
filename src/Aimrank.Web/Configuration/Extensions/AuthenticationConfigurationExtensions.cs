using System.Linq;
using Aimrank.Infrastructure.Configuration.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Aimrank.Web.Configuration.Extensions
{
    public static class AuthenticationConfigurationExtensions
    {
        public static IServiceCollection AddAuthenticationWithBearer(this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);

            services.AddAuthorization();
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddSteam(options =>
                {
                    options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
                    options.SignInScheme = "Cookies";
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret))
                    };

                    var hubs = new[] {"/hubs/general"};

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            if (string.IsNullOrEmpty(accessToken))
                            {
                                return Task.CompletedTask;
                            }

                            if (hubs.Any(h => context.HttpContext.Request.Path.StartsWithSegments(h)))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                })
                .AddCookie("Cookies");

            return services;
        }
    }
}