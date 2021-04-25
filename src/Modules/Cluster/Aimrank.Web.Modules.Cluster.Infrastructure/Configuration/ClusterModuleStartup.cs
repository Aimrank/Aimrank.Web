using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Infrastructure;
using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.EventBus;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Pods;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Quartz;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration
{
    public class ClusterModuleStartup : IModuleStartup
    {
        public void Register(IServiceCollection services)
        {
            services.AddSingleton<IClusterModule, ClusterModule>();
        }

        public void Initialize(IApplicationBuilder builder, IConfiguration configuration)
        {
            var eventBus = builder.ApplicationServices.GetRequiredService<IEventBus>();
            
            ILogger logger = builder.ApplicationServices.GetRequiredService<ILogger<ClusterModule>>();
            
            var containerBuilder = new ContainerBuilder();
            
            containerBuilder.RegisterModule(new DataAccessModule(configuration.GetConnectionString("Database")));
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new PodsModule());
            containerBuilder.RegisterInstance(builder.ApplicationServices.GetRequiredService<IHttpClientFactory>());
            containerBuilder.RegisterInstance(eventBus);
            containerBuilder.RegisterInstance(logger);
            
            ClusterCompositionRoot.SetContainer(containerBuilder.Build());
            
            EventBusStartup.Initialize(eventBus);
            QuartzStartup.Initialize();
        }
    }
}