using Aimrank.Web.App.Configuration.EventBus.RabbitMQ;
using Aimrank.Web.App.GraphQL.Subscriptions.Lobbies.Events;
using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Lobbies;
using Aimrank.Web.Modules.Matches.IntegrationEvents.Matches;
using Microsoft.Extensions.DependencyInjection;

namespace Aimrank.Web.App.Configuration.EventBus
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.AddSingleton<IEventBus, InMemoryEventBus>();
            services.Decorate<IEventBus, RabbitMQEventBus>();

            RegisterEventHandler<LobbyStatusChangedEvent, LobbyStatusChangedEventHandler>(services);
            RegisterEventHandler<MatchAcceptedEvent, MatchAcceptedEventHandler>(services);
            RegisterEventHandler<MatchCanceledEvent, MatchCanceledEventHandler>(services);
            RegisterEventHandler<MatchFinishedEvent, MatchFinishedEventHandler>(services);
            RegisterEventHandler<MatchPlayerLeftEvent, MatchPlayerLeftEventHandler>(services);
            RegisterEventHandler<MatchReadyEvent, MatchReadyEventHandler>(services);
            RegisterEventHandler<MatchStartedEvent, MatchStartedEventHandler>(services);
            RegisterEventHandler<MatchStartingEvent, MatchStartingEventHandler>(services);
            RegisterEventHandler<MatchTimedOutEvent, MatchTimedOutEventHandler>(services);
            RegisterEventHandler<MemberLeftEvent, MemberLeftEventHandler>(services);
            RegisterEventHandler<MemberRoleChangedEvent, MemberRoleChangedEventHandler>(services);
            
            return services;
        }

        private static void RegisterEventHandler<TEvent, TEventHandler>(IServiceCollection services)
            where TEvent : class, IIntegrationEvent
            where TEventHandler : class, IIntegrationEventHandler<TEvent>
        {
            services.AddScoped<IIntegrationEventHandler<TEvent>, TEventHandler>();
            services.AddScoped<IIntegrationEventHandler>(p =>
                p.GetRequiredService<IIntegrationEventHandler<TEvent>>());
        }
    }
}