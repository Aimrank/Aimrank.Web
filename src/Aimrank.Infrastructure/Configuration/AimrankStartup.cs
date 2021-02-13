using Aimrank.Common.Application;
using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Infrastructure.Configuration.Authentication;
using Aimrank.Infrastructure.Configuration.CSGO;
using Aimrank.Infrastructure.Configuration.DataAccess;
using Aimrank.Infrastructure.Configuration.Jwt;
using Aimrank.Infrastructure.Configuration.Mediator;
using Aimrank.Infrastructure.Configuration.Processing;
using Aimrank.Infrastructure.Configuration.Quartz;
using Aimrank.Infrastructure.Configuration.Redis;
using Autofac;

namespace Aimrank.Infrastructure.Configuration
{
    public static class AimrankStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus,
            JwtSettings jwtSettings,
            CSGOSettings csgoSettings,
            RedisSettings redisSettings)
        {
            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor,
                eventBus,
                jwtSettings,
                csgoSettings,
                redisSettings);
            
            QuartzStartup.Initialize();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            IEventBus eventBus,
            JwtSettings jwtSettings,
            CSGOSettings csgoSettings,
            RedisSettings redisSettings)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new AuthenticationModule());
            containerBuilder.RegisterModule(new CSGOModule(csgoSettings));
            containerBuilder.RegisterModule(new JwtModule(jwtSettings));
            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new RedisModule(redisSettings));
            containerBuilder.RegisterInstance(executionContextAccessor);
            containerBuilder.RegisterInstance(eventBus);

            _container = containerBuilder.Build();
            
            AimrankCompositionRoot.SetContainer(_container);
        }
    }
}