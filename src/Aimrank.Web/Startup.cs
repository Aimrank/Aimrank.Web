using Aimrank.Common.Application;
using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Infrastructure.Configuration.CSGO;
using Aimrank.Infrastructure.Configuration.Jwt;
using Aimrank.Infrastructure.Configuration.Redis;
using Aimrank.Infrastructure.Configuration;
using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.IntegrationEvents.Matches;
using Aimrank.Web.Configuration.ExecutionContext;
using Aimrank.Web.Configuration.Extensions;
using Aimrank.Web.Configuration;
using Aimrank.Web.Events.Handlers.Lobbies;
using Aimrank.Web.Events.Handlers.Matches;
using Aimrank.Web.Events.Handlers;
using Aimrank.Web.GraphQL.Mutations;
using Aimrank.Web.GraphQL.Queries;
using Aimrank.Web.GraphQL.Subscriptions;
using Aimrank.Web.GraphQL;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aimrank.Web
{
    public class Startup
    {
        private readonly IEventBus _eventBus = new InMemoryEventBusClient();
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddAuthenticationWithBearer(_configuration);

            services
                .AddGraphQLServer()
                .AddQueryType<QueryType>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddErrorFilter<GraphQLErrorFilter>()
                .AddInMemorySubscriptions()
                .AddAuthorization();

            services.AddControllersWithViews();
            services.AddRouting(options => options.LowercaseUrls = true);

            /* services.AddDbContext<AimrankContext>(options =>
            {
                options.ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>();
                options.UseSqlServer(_configuration.GetConnectionString("Database"),
                    x => x.MigrationsAssembly("Aimrank.Database.Migrator"));
            }); */
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new AimrankAutofacModule());

            containerBuilder.RegisterType<MatchReadyEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<MatchAcceptedEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<MatchTimedOutEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<MatchStartingEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<MatchStartedEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<MatchCanceledEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<MatchFinishedEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<MatchPlayerLeftEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<LobbyStatusChangedEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<MemberLeftEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<MemberRoleChangedEventHandler>().AsImplementedInterfaces();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var container = app.ApplicationServices.GetAutofacRoot();
            
            InitializeEventBus(container);
            InitializeModules(container);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseWebSockets();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{*all}",
                    defaults: new {Controller = "Home", Action = "Index"});
            });
        }
        private void InitializeEventBus(ILifetimeScope container)
        {
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MatchReadyEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MatchAcceptedEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MatchTimedOutEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MatchStartingEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MatchStartedEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MatchCanceledEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MatchFinishedEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MatchPlayerLeftEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<LobbyStatusChangedEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MemberLeftEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MemberRoleChangedEvent>(container));
        }

        private void InitializeModules(ILifetimeScope container)
        {
            var executionContextAccessor = container.Resolve<IExecutionContextAccessor>();
            var jwtSettings = new JwtSettings();
            var csgoSettings = new CSGOSettings();
            var redisSettings = new RedisSettings();
            _configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
            _configuration.GetSection(nameof(CSGOSettings)).Bind(csgoSettings);
            _configuration.GetSection(nameof(RedisSettings)).Bind(redisSettings);

            var connectionString = _configuration.GetConnectionString("Database");
            
            AimrankStartup.Initialize(
                connectionString,
                executionContextAccessor,
                _eventBus,
                jwtSettings,
                csgoSettings,
                redisSettings);
        }
    }
}