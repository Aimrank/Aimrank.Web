using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Emails;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Quartz;
using Autofac;
using Microsoft.Extensions.Logging;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration
{
    public static class UserAccessStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            UserAccessModuleSettings userAccessModuleSettings,
            ILogger logger,
            IExecutionContextAccessor executionContextAccessor,
            IUrlFactory urlFactory)
        {
            ConfigureCompositionRoot(
                connectionString,
                userAccessModuleSettings,
                logger,
                executionContextAccessor,
                urlFactory);
            
            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            UserAccessModuleSettings userAccessModuleSettings,
            ILogger logger,
            IExecutionContextAccessor executionContextAccessor,
            IUrlFactory urlFactory)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EmailModule(userAccessModuleSettings.EmailSettings));
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterInstance(executionContextAccessor);
            containerBuilder.RegisterInstance(urlFactory);
            containerBuilder.RegisterInstance(logger);

            _container = containerBuilder.Build();
            
            UserAccessCompositionRoot.SetContainer(_container);
        }
    }
}