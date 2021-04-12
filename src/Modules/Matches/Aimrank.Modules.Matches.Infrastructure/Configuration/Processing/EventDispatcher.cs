using Aimrank.Common.Application.Events;
using Aimrank.Common.Domain;
using Aimrank.Common.Infrastructure;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Outbox;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration.Processing
{
    internal class EventDispatcher : IEventDispatcher
    {
        private readonly MatchesContext _context;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IEventMapper _eventMapper;
        private readonly IDomainEventAccessor _domainEventAccessor;

        public EventDispatcher(
            MatchesContext context,
            ILifetimeScope lifetimeScope,
            IEventMapper eventMapper,
            IDomainEventAccessor domainEventAccessor)
        {
            _context = context;
            _lifetimeScope = lifetimeScope;
            _eventMapper = eventMapper;
            _domainEventAccessor = domainEventAccessor;
        }

        public async Task DispatchAsync()
        {
            var domainEvents = new Queue<IDomainEvent>(_domainEventAccessor.GetDomainEvents());
            
            var integrationEvents = new List<IIntegrationEvent>();

            while (domainEvents.Count > 0)
            {
                var domainEvent = domainEvents.Dequeue();
                var domainEventType = domainEvent.GetType();
                var domainEventHandlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEventType);
                var domainEventHandlersCollectionType = typeof(IEnumerable<>).MakeGenericType(domainEventHandlerType);

                dynamic handlers = _lifetimeScope.Resolve(domainEventHandlersCollectionType);

                foreach (var handler in handlers)
                {
                    await handler.HandleAsync((dynamic) domainEvent);

                    foreach (var newEvent in _domainEventAccessor.GetDomainEvents())
                    {
                        domainEvents.Enqueue(newEvent);
                    }
                }

                var integrationEvent = _eventMapper.Map(domainEvent);
                if (integrationEvent is not null)
                {
                    integrationEvents.Add(integrationEvent);
                }
            }
            
            AddIntegrationEventsToOutbox(integrationEvents);
        }

        public Task DispatchAsync(IIntegrationEvent @event)
        {
            AddIntegrationEventsToOutbox(@event);
            return Task.CompletedTask;
        }

        private void AddIntegrationEventsToOutbox(IEnumerable<IIntegrationEvent> events)
        {
            foreach (var integrationEvent in events)
            {
                var integrationEventType = integrationEvent.GetType().FullName;
                var integrationEventData = JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType());
                var outboxMessage = new OutboxMessage(
                    integrationEvent.Id,
                    integrationEvent.OccurredAt,
                    integrationEventType,
                    integrationEventData);
                _context.OutboxMessages.Add(outboxMessage);
            }
        }

        private void AddIntegrationEventsToOutbox(params IIntegrationEvent[] events)
            => AddIntegrationEventsToOutbox(events.AsEnumerable());
    }
}