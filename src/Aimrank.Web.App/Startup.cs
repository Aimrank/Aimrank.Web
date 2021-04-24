using Aimrank.Web.App.Configuration.EventBus.RabbitMQ;
using Aimrank.Web.App.Configuration.EventBus;
using Aimrank.Web.App.Configuration.ExecutionContext;
using Aimrank.Web.App.Configuration.SessionAuthentication;
using Aimrank.Web.App.Configuration.UrlFactory;
using Aimrank.Web.App.GraphQL;
using Aimrank.Web.App.Modules.Cluster;
using Aimrank.Web.App.Modules.Matches;
using Aimrank.Web.App.Modules.UserAccess;
using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration;
using Aimrank.Web.Modules.Cluster.Infrastructure;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration;
using Aimrank.Web.Modules.Matches.Infrastructure;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration;
using Aimrank.Web.Modules.UserAccess.Infrastructure;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Net.Http;

namespace Aimrank.Web.App
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var redisSettings = Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();
            var urlFactorySettings = Configuration.GetSection(nameof(UrlFactorySettings)).Get<UrlFactorySettings>();
            
            services.AddHttpClient();

            services.AddSingleton(urlFactorySettings);
            services.AddSingleton<IUrlFactory, ApplicationUrlFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = new ConfigurationOptions
                {
                    EndPoints = {redisSettings.Endpoint},
                    DefaultDatabase = redisSettings.Database
                };
            });

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthorization();
            services.AddAuthentication(SessionAuthenticationDefaults.AuthenticationScheme)
                .AddSession()
                .AddSteam(options =>
                {
                    options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
                    options.SignInScheme = "Cookies";
                })
                .AddCookie();

            services.AddApplicationGraphQL();

            services.AddControllersWithViews();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddRabbitMQ();

            #region Migrations
#if false
            //Add db contexts so "dotnet ef" can find them when generating migrations
            void AddDbContext<T>() where T : DbContext
            {
                services.AddDbContext<T>(options =>
                {
                    options.ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>();
                    options
                        .UseNpgsql(Configuration.GetConnectionString("Database"),
                            x => x.MigrationsAssembly("Aimrank.Web.Database.Migrator"))
                        .UseSnakeCaseNamingConvention();
                });
            }
            
            AddDbContext<ClusterContext>();
            AddDbContext<MatchesContext>();
            AddDbContext<UserAccessContext>();
#endif
            #endregion
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new EventBusAutofacModule(Configuration));
            containerBuilder.RegisterModule(new ClusterAutofacModule());
            containerBuilder.RegisterModule(new MatchesAutofacModule());
            containerBuilder.RegisterModule(new UserAccessAutofacModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            InitializeEventBus(container);
            InitializeModules(container);

            app.UseRabbitMQ();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseWebSockets();
            
            app.UseSession();
            
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

        private void InitializeModules(ILifetimeScope container)
        {
            var eventBus = container.Resolve<IEventBus>();
            var clusterModule = container.Resolve<IClusterModule>();
            var urlFactory = container.Resolve<IUrlFactory>();
            var httpClientFactory = container.Resolve<IHttpClientFactory>();
            var executionContextAccessor = container.Resolve<IExecutionContextAccessor>();
            
            var connectionString = Configuration.GetConnectionString("Database");
            var matchesModuleSettings = Configuration.GetSection(nameof(MatchesModuleSettings)).Get<MatchesModuleSettings>();
            var userAccessModuleSettings = Configuration.GetSection(nameof(UserAccessModuleSettings)).Get<UserAccessModuleSettings>();
            
            ClusterStartup.Initialize(
                connectionString,
                container.Resolve<ILogger<ClusterModule>>(),
                httpClientFactory,
                eventBus);

            MatchesStartup.Initialize(
                connectionString,
                matchesModuleSettings,
                container.Resolve<ILogger<MatchesModule>>(),
                clusterModule,
                executionContextAccessor,
                eventBus);
            
            UserAccessStartup.Initialize(
                connectionString,
                userAccessModuleSettings,
                container.Resolve<ILogger<UserAccessModule>>(),
                executionContextAccessor,
                urlFactory);
        }
        
        private static void InitializeEventBus(ILifetimeScope container)
        {
            container.Resolve<IEventBus>()
                .Subscribe(new IntegrationEventGenericHandler<MatchReadyEvent>(container))
                .Subscribe(new IntegrationEventGenericHandler<MatchAcceptedEvent>(container))
                .Subscribe(new IntegrationEventGenericHandler<MatchTimedOutEvent>(container))
                .Subscribe(new IntegrationEventGenericHandler<MatchStartingEvent>(container))
                .Subscribe(new IntegrationEventGenericHandler<MatchStartedEvent>(container))
                .Subscribe(new IntegrationEventGenericHandler<MatchCanceledEvent>(container))
                .Subscribe(new IntegrationEventGenericHandler<MatchFinishedEvent>(container))
                .Subscribe(new IntegrationEventGenericHandler<MatchPlayerLeftEvent>(container))
                .Subscribe(new IntegrationEventGenericHandler<LobbyStatusChangedEvent>(container))
                .Subscribe(new IntegrationEventGenericHandler<MemberLeftEvent>(container))
                .Subscribe(new IntegrationEventGenericHandler<MemberRoleChangedEvent>(container));
        }
    }
}