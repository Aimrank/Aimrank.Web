using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Web.App.Configuration.EventBus.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));
            services.AddSingleton<RabbitMQEventRegistry>();
            services.AddSingleton<RabbitMQEventSerializer>();
            services.AddSingleton<RabbitMQRoutingKeyFactory>();
            services.AddHostedService<RabbitMQBackgroundService>();
            
            return services;
        }
    }
}