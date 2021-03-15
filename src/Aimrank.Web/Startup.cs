using Aimrank.Common.Application;
using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Modules.Matches.Infrastructure.Configuration.CSGO;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Redis;
using Aimrank.Modules.Matches.Infrastructure.Configuration;
using Aimrank.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Modules.UserAccess.Infrastructure.Configuration;
using Aimrank.Web.Configuration.EventBus;
using Aimrank.Web.Configuration.ExecutionContext;
using Aimrank.Web.GraphQL.Mutations;
using Aimrank.Web.GraphQL.Queries;
using Aimrank.Web.GraphQL.Subscriptions;
using Aimrank.Web.GraphQL;
using Aimrank.Web.Modules.Matches;
using Aimrank.Web.Modules.UserAccess;
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

            services.AddAuthorization();
            services.AddAuthentication()
                .AddSteam(options =>
                {
                    options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
                    options.SignInScheme = "Cookies";
                })
                .AddCookie();

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

#if false
            //Add db contexts so "dotnet ef" can find them when generating migrations

            services.AddDbContext<MatchesContext>(options =>
            {
                options.ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>();
                options.UseSqlServer(_configuration.GetConnectionString("Database"),
                    x => x.MigrationsAssembly("Aimrank.Database.Migrator"));
            });
            
            services.AddDbContext<UserAccessContext>(options =>
            {
                options.ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>();
                options.UseSqlServer(_configuration.GetConnectionString("Database"),
                    x => x.MigrationsAssembly("Aimrank.Database.Migrator"));
            });
#endif
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new MatchesAutofacModule());
            containerBuilder.RegisterModule(new UserAccessAutofacModule());
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
            
            var csgoSettings = new CSGOSettings();
            var redisSettings = new RedisSettings();
            _configuration.GetSection(nameof(CSGOSettings)).Bind(csgoSettings);
            _configuration.GetSection(nameof(RedisSettings)).Bind(redisSettings);

            var connectionString = _configuration.GetConnectionString("Database");
            
            MatchesStartup.Initialize(
                connectionString,
                executionContextAccessor,
                _eventBus,
                csgoSettings,
                redisSettings);
            
            UserAccessStartup.Initialize(
                connectionString,
                executionContextAccessor);
        }
    }
}