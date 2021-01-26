using Aimrank.Application.Events;
using Aimrank.Application.Exceptions;
using Aimrank.Application;
using Aimrank.Infrastructure;
using Aimrank.Infrastructure.Configuration;
using Aimrank.Infrastructure.EventBus;
using Aimrank.Web.Configuration.ExecutionContext;
using Aimrank.Web.Configuration.Extensions;
using Aimrank.Web.Configuration;
using Aimrank.Web.Events.Handlers;
using Aimrank.Web.Hubs;
using Aimrank.Web.ProblemDetails;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

            services.AddSignalR();
            services.AddSwagger();
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (_, _) => false;
                options.Map<ApplicationException>(ex => new ApplicationProblemDetails(ex));
            });

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllersWithViews();

            // services.AddDbContext<AimrankContext>(options =>
            //     options.UseSqlServer(_configuration.GetConnectionString("Database"), 
            //         x => x.MigrationsAssembly("Aimrank.Database.Migrator")));
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new AimrankAutofacModule());

            containerBuilder.RegisterType<ServerMessageReceivedEventHandler>().AsImplementedInterfaces();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var container = app.ApplicationServices.GetAutofacRoot();
            
            InitializeEventBus(container);
            InitializeModules(container);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aimrank API v1"));
            }

            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseProblemDetails();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<GameHub>("/hubs/game");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{*all}",
                    defaults: new {Controller = "Home", Action = "Index"});
            });
        }
        private void InitializeEventBus(ILifetimeScope container)
        {
            _eventBus.Subscribe(new IntegrationEventGenericHandler<ServerMessageReceivedEvent>(container));
        }

        private void InitializeModules(ILifetimeScope container)
        {
            var executionContextAccessor = container.Resolve<IExecutionContextAccessor>();

            var connectionString = _configuration.GetConnectionString("Database");
            
            AimrankStartup.Initialize(
                connectionString,
                executionContextAccessor,
                _eventBus);
        }
    }
}