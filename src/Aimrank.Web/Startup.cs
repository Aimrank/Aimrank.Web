using Aimrank.Common.Application;
using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Modules.CSGO.Application.Contracts;
using Aimrank.Modules.CSGO.Infrastructure.Configuration.Rabbit;
using Aimrank.Modules.CSGO.Infrastructure.Configuration;
using Aimrank.Modules.Matches.Infrastructure.Configuration;
using Aimrank.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Modules.Matches.IntegrationEvents.Matches;
using Aimrank.Modules.UserAccess.Application.Services;
using Aimrank.Modules.UserAccess.Infrastructure.Configuration;
using Aimrank.Web.Configuration.EventBus;
using Aimrank.Web.Configuration.ExecutionContext;
using Aimrank.Web.Configuration.SessionAuthentication;
using Aimrank.Web.Configuration.UrlFactory;
using Aimrank.Web.GraphQL;
using Aimrank.Web.Modules.CSGO;
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
using StackExchange.Redis;
using System.Net.Http;

namespace Aimrank.Web
{
    public class Startup
    {
        private readonly IEventBus _eventBus = new InMemoryEventBusClient();
        
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            
            services.AddSingleton<IUrlFactory, ApplicationUrlFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
            
            var redisSettings = Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();

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

#if false
            //Add db contexts so "dotnet ef" can find them when generating migrations
            var connectionString = Configuration.GetConnectionString("Database");

            services.AddDbContext<CSGOContext>(options =>
                options.UseSqlServer(connectionString,
                    x => x.MigrationsAssembly("Aimrank.Database.Migrator")));

            services.AddDbContext<MatchesContext>(options =>
            {
                options.ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>();
                options.UseSqlServer(connectionString,
                    x => x.MigrationsAssembly("Aimrank.Database.Migrator"));
            });
            
            services.AddDbContext<UserAccessContext>(options =>
            {
                options.ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>();
                options.UseSqlServer(connectionString,
                    x => x.MigrationsAssembly("Aimrank.Database.Migrator"));
            });
#endif
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new CSGOAutofacModule());
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
            var csgoModule = container.Resolve<ICSGOModule>();
            var urlFactory = container.Resolve<IUrlFactory>();
            var httpClientFactory = container.Resolve<IHttpClientFactory>();
            var executionContextAccessor = container.Resolve<IExecutionContextAccessor>();
            
            var connectionString = Configuration.GetConnectionString("Database");
            var rabbitMqSettings = Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
            var matchesModuleSettings =Configuration.GetSection(nameof(MatchesModuleSettings)).Get<MatchesModuleSettings>();
            var userAccessModuleSettings = Configuration.GetSection(nameof(UserAccessModuleSettings)).Get<UserAccessModuleSettings>();
            
            CSGOStartup.Initialize(
                connectionString,
                httpClientFactory,
                rabbitMqSettings);

            MatchesStartup.Initialize(
                connectionString,
                csgoModule,
                executionContextAccessor,
                _eventBus,
                matchesModuleSettings);
            
            UserAccessStartup.Initialize(
                connectionString,
                executionContextAccessor,
                urlFactory,
                userAccessModuleSettings);
        }
    }
}