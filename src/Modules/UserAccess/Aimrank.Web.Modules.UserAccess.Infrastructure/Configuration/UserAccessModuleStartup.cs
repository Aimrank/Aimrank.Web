using Aimrank.Web.Common.Application;
using Aimrank.Web.Common.Infrastructure;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Emails;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Quartz;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration
{
    public class UserAccessModuleStartup : IModuleStartup
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUserAccessModule, UserAccessModule>();
        }
        
        public void Initialize(IApplicationBuilder builder, IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(UserAccessModuleSettings)).Get<UserAccessModuleSettings>();
            
            ILogger logger = builder.ApplicationServices.GetRequiredService<ILogger<UserAccessModule>>();
            
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(configuration.GetConnectionString("Database")));
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EmailModule(settings.EmailSettings));
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterInstance(builder.ApplicationServices.GetRequiredService<IExecutionContextAccessor>());
            containerBuilder.RegisterInstance(builder.ApplicationServices.GetRequiredService<IUrlFactory>());
            containerBuilder.RegisterInstance(logger);
            
            UserAccessCompositionRoot.SetContainer(containerBuilder.Build());
            
            QuartzStartup.Initialize();
        }
    }
}