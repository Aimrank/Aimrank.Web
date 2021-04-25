using Aimrank.Web.App.Configuration.EventBus.RabbitMQ;
using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Web.App.Configuration.EventBus
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.AddSingleton<IEventBus, InMemoryEventBusClient>();
            services.Decorate<IEventBus, RabbitMQEventBus>();

            services.Scan(scan => scan
                .FromAssemblyOf<Startup>()
                .AddClasses(classes =>
                    classes.Where(x =>
                        typeof(IIntegrationEventHandler).IsAssignableFrom(x) &&
                        !x.IsInterface &&
                        !x.IsAbstract &&
                        !x.IsGenericType))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            
            return services;
        }

        public static IApplicationBuilder UseEventBus(this IApplicationBuilder builder)
        {
            var factory = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var eventBus = builder.ApplicationServices.GetRequiredService<IEventBus>();
                
            eventBus
                .Subscribe(new IntegrationEventGenericHandler<MatchReadyEvent>(factory))
                .Subscribe(new IntegrationEventGenericHandler<MatchAcceptedEvent>(factory))
                .Subscribe(new IntegrationEventGenericHandler<MatchTimedOutEvent>(factory))
                .Subscribe(new IntegrationEventGenericHandler<MatchStartingEvent>(factory))
                .Subscribe(new IntegrationEventGenericHandler<MatchStartedEvent>(factory))
                .Subscribe(new IntegrationEventGenericHandler<MatchCanceledEvent>(factory))
                .Subscribe(new IntegrationEventGenericHandler<MatchFinishedEvent>(factory))
                .Subscribe(new IntegrationEventGenericHandler<MatchPlayerLeftEvent>(factory))
                .Subscribe(new IntegrationEventGenericHandler<LobbyStatusChangedEvent>(factory))
                .Subscribe(new IntegrationEventGenericHandler<MemberLeftEvent>(factory))
                .Subscribe(new IntegrationEventGenericHandler<MemberRoleChangedEvent>(factory));

            return builder;
        }
    }
}