using Aimrank.Web.App.Configuration.EventBus.RabbitMQ;
using Aimrank.Web.App.Configuration.EventBus;
using Aimrank.Web.App.Configuration.ExecutionContext;
using Aimrank.Web.App.Configuration.SessionAuthentication;
using Aimrank.Web.App.Configuration.UrlFactory;
using Aimrank.Web.App.Configuration;
using Aimrank.Web.App.GraphQL;
using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

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
            
            services.AddHttpClient();

            services.Configure<UrlFactorySettings>(Configuration.GetSection(nameof(UrlFactorySettings)));
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
            
            services.AddModules();
            services.AddEventBus();
            services.AddRabbitMQ(Configuration);

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app.UseModules(Configuration)
                .UseEventBus()
                .UseRabbitMQ()
                .UseStaticFiles()
                .UseRouting()
                .UseWebSockets()
                .UseSession()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGraphQL();
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{*all}",
                        defaults: new {Controller = "Home", Action = "Index"});
                });
    }
}