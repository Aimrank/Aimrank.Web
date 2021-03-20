using Aimrank.Common.Application.Events;
using Aimrank.Common.Domain;
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
        private readonly IEventMapper _eventMapper;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly MatchesContext _context;

        public EventDispatcher(
            IEventMapper eventMapper,
            ILifetimeScope lifetimeScope,
            MatchesContext context)
        {
            _eventMapper = eventMapper;
            _lifetimeScope = lifetimeScope;
            _context = context;
        }

        public async Task DispatchAsync()
        {
            var domainEvents = new Queue<IDomainEvent>(GetDomainEventsFromTrackedEntities());
            
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

                    foreach (var newEvent in GetDomainEventsFromTrackedEntities())
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

        public void Dispatch(IIntegrationEvent @event) => AddIntegrationEventsToOutbox(@event);

        private IEnumerable<IDomainEvent> GetDomainEventsFromTrackedEntities()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents.Any())
                .ToList();
            
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var entity in domainEntities)
            {
                entity.Entity.ClearDomainEvents();
            }

            return domainEvents;
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