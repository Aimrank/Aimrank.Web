using Aimrank.Application.Commands.SignIn;
using Aimrank.Application.Commands.SignUp;
using Aimrank.Common.Application.Exceptions;
using Aimrank.Common.Application;
using Aimrank.Common.Domain;
using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Common.Infrastructure;
using Aimrank.Infrastructure.Configuration.CSGO;
using Aimrank.Infrastructure.Configuration.Jwt;
using Aimrank.Infrastructure.Configuration;
using Aimrank.Infrastructure;
using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.IntegrationEvents;
using Aimrank.Web.Configuration.ExecutionContext;
using Aimrank.Web.Configuration.Extensions;
using Aimrank.Web.Configuration;
using Aimrank.Web.Events.Handlers.Lobbies;
using Aimrank.Web.Events.Handlers;
using Aimrank.Web.Hubs;
using Aimrank.Web.ProblemDetails;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            services.AddScoped<IUserIdProvider, HubUserIdProvider>();

            services.AddSignalR();
            services.AddSwagger();
            services.AddAuthenticationWithBearer(_configuration);
            services.AddProblemDetails(options =>
            {
                options.ExceptionDetailsPropertyName = "exceptionDetails";
                options.Map<SignUpException>(ex => new SignUpProblemDetails(ex));
                options.Map<SignInException>(ex => new SignInProblemDetails(ex));
                options.Map<ApplicationException>(ex => new ApplicationProblemDetails(ex));
                options.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationProblemDetails(ex));
            });

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllersWithViews()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>(lifetime: ServiceLifetime.Singleton);
                    options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    options.ImplicitlyValidateChildProperties = true;
                    options.LocalizationEnabled = false;
                });

            // services.AddDbContext<AimrankContext>(options =>
            // {
            //     options.ReplaceService<IValueConverterSelector, EntityIdValueConverterSelector>();
            //     options.UseSqlServer(_configuration.GetConnectionString("Database"),
            //         x => x.MigrationsAssembly("Aimrank.Database.Migrator"));
            // });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new AimrankAutofacModule());

            containerBuilder.RegisterType<MatchStartingEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<MatchStartedEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<MatchFinishedEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<InvitationAcceptedEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<InvitationCanceledEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<InvitationCreatedEventHandler>().AsImplementedInterfaces();
            containerBuilder.RegisterType<LobbyConfigurationChangedEventHandler>().AsImplementedInterfaces();
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
                endpoints.MapHub<GeneralHub>("/hubs/general");
                endpoints.MapHub<LobbyHub>("/hubs/lobby");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{*all}",
                    defaults: new {Controller = "Home", Action = "Index"});
            });
        }
        private void InitializeEventBus(ILifetimeScope container)
        {
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MatchStartingEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MatchStartedEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MatchFinishedEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<InvitationAcceptedEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<InvitationCanceledEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<InvitationCreatedEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<LobbyConfigurationChangedEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<LobbyStatusChangedEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MemberLeftEvent>(container));
            _eventBus.Subscribe(new IntegrationEventGenericHandler<MemberRoleChangedEvent>(container));
        }

        private void InitializeModules(ILifetimeScope container)
        {
            var executionContextAccessor = container.Resolve<IExecutionContextAccessor>();
            var jwtSettings = new JwtSettings();
            var csgoSettings = new CSGOSettings();
            _configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
            _configuration.GetSection(nameof(CSGOSettings)).Bind(csgoSettings);

            var connectionString = _configuration.GetConnectionString("Database");
            
            AimrankStartup.Initialize(
                connectionString,
                executionContextAccessor,
                _eventBus,
                jwtSettings,
                csgoSettings);
        }
    }
}