using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;

namespace Aimrank.Web.App.Configuration.Authentication
{
    public static class Extensions
    {
        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var redisSettings = configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();
            var redisOptions = new ConfigurationOptions
            {
                EndPoints = {redisSettings.Endpoint},
                DefaultDatabase = redisSettings.Database
            };

            var redisCache = new RedisCache(new RedisCacheOptions {ConfigurationOptions = redisOptions});
            var redisCacheTicketStore = new RedisCacheTicketStore(redisCache);
            
            services.AddSingleton<IDistributedCache>(redisCache);
            services.AddSingleton(redisCacheTicketStore);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(c =>
                {
                    c.ExpireTimeSpan = TimeSpan.FromDays(1);
                    c.SessionStore = redisCacheTicketStore;
                    c.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                })
                .AddSteam(options =>
                {
                    options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
                    options.SignInScheme = "CookiesSteam";
                })
                .AddCookie("CookiesSteam");

            services.AddAuthorization();

            return services;
        }
    }
}