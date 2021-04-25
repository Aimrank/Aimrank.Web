using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace Aimrank.Web.App.Configuration.EventBus.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));
            services.AddSingleton<RabbitMQEventSerializer>();
            services.AddSingleton<RabbitMQRoutingKeyFactory>();
            services.AddHostedService<RabbitMQBackgroundService>();
            
            return services;
        }
        
        public static IApplicationBuilder UseRabbitMQ(this IApplicationBuilder builder)
        {
            var backgroundService = builder.ApplicationServices
                .GetRequiredService<IEnumerable<IHostedService>>()
                .Single(s => s.GetType() == typeof(RabbitMQBackgroundService));

            if (backgroundService is RabbitMQBackgroundService service)
            {
                service.Configure();
            }
            
            return builder;
        }
    }
}