using Aimrank.Web.App.Configuration.Clients.Cluster;
using Aimrank.Web.Modules.Matches.Application.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aimrank.Web.App.Configuration.Clients
{
    public static class Extensions
    {
        public static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.IsDevelopment() || configuration.IsDocker())
            {
                services.AddSingleton<IClusterClient, FakeClusterClient>();
            }
            else
            {
                var settings = configuration.GetSection(nameof(ClusterSettings)).Get<ClusterSettings>();
                services.AddHttpClient<IClusterClient, ClusterClient>(c =>
                {
                    c.BaseAddress = new Uri($"http://{settings.HostName}/api/");
                });
            }
            
            return services;
        }
    }
}