using Aimrank.Web.App.Configuration.Authentication;
using Aimrank.Web.App.Configuration.Clients;
using Aimrank.Web.App.Configuration.EventBus.RabbitMQ;
using Aimrank.Web.App.Configuration.EventBus;
using Aimrank.Web.App.Configuration.ExecutionContext;
using Aimrank.Web.App.Configuration.UrlFactory;
using Aimrank.Web.App.Configuration;
using Aimrank.Web.App.GraphQL;
using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddCookieAuthentication(Configuration);
            
            services.Configure<UrlFactorySettings>(Configuration.GetSection(nameof(UrlFactorySettings)));
            services.AddSingleton<IUrlFactory, ApplicationUrlFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddApplicationGraphQL();
            services.AddControllersWithViews();
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddClients(Configuration);
            services.AddModules(Configuration);
            services.AddEventBus();
            services.AddRabbitMQ(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                })
                .UseModules(Configuration)
                .UseRabbitMQ()
                .UseStaticFiles()
                .UseRouting()
                .UseWebSockets()
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