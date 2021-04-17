using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Emails;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Mediator;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Quartz;
using Autofac;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration
{
    public static class UserAccessStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IUrlFactory urlFactory,
            UserAccessModuleSettings userAccessModuleSettings)
        {
            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor,
                urlFactory,
                userAccessModuleSettings);
            
            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IUrlFactory urlFactory,
            UserAccessModuleSettings userAccessModuleSettings)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EmailModule(userAccessModuleSettings.EmailSettings));
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterInstance(executionContextAccessor);
            containerBuilder.RegisterInstance(urlFactory);

            _container = containerBuilder.Build();
            
            UserAccessCompositionRoot.SetContainer(_container);
        }
    }
}