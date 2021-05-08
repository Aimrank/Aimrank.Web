using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Redis
{
    internal static class Extensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, RedisSettings redisSettings)
        {
            var connectionMultiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = {redisSettings.Endpoint},
                DefaultDatabase = redisSettings.Database
            });

            services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);

            return services;
        }
    }
}