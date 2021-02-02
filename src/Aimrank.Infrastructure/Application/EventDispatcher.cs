using Aimrank.Application;
using Aimrank.Common.Application;
using Aimrank.Common.Domain;
using Aimrank.Common.Infrastructure.EventBus;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Application
{
    internal class EventDispatcher : IEventDispatcher
    {
        private readonly IEventBus _eventBus;
        private readonly IEventMapper _eventMapper;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly AimrankContext _context;

        public EventDispatcher(
            IEventBus eventBus,
            IEventMapper eventMapper,
            ILifetimeScope lifetimeScope,
            AimrankContext context)
        {
            _eventBus = eventBus;
            _eventMapper = eventMapper;
            _lifetimeScope = lifetimeScope;
            _context = context;
        }

        public async Task DispatchAsync()
        {
            var domainEvents = new Queue<IDomainEvent>(GetDomainEventsFromTrackedEntities());
            
            var integrationEvents = new List<IntegrationEvent>();

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

            foreach (var integrationEvent in integrationEvents)
            {
                await _eventBus.Publish(integrationEvent);
            }
        }

        public Task DispatchAsync(IntegrationEvent @event) => _eventBus.Publish(@event);

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
    }
}