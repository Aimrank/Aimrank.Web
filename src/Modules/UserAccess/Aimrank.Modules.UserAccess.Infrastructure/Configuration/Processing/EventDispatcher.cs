using Aimrank.Common.Application.Events;
using Aimrank.Common.Domain;
using Aimrank.Common.Infrastructure;
using Autofac;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal class EventDispatcher : IEventDispatcher
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IDomainEventAccessor _domainEventAccessor;

        public EventDispatcher(
            UserAccessContext context,
            ILifetimeScope lifetimeScope,
            IDomainEventAccessor domainEventAccessor)
        {
            _lifetimeScope = lifetimeScope;
            _domainEventAccessor = domainEventAccessor;
        }

        public void Dispatch(IIntegrationEvent @event)
        {
        }

        public async Task DispatchAsync()
        {
            var domainEvents = new Queue<IDomainEvent>(_domainEventAccessor.GetDomainEvents());
            
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
            }
        }
    }
}