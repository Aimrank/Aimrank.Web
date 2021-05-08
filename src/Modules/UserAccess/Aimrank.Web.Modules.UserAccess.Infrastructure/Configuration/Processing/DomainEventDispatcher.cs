using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Domain;
using Aimrank.Web.Common.Infrastructure;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;
using MediatR;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal class DomainEventDispatcher
    {
        private readonly IDomainEventAccessor _domainEventAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;
        private readonly UserAccessContext _context;

        public DomainEventDispatcher(
            IDomainEventAccessor domainEventAccessor,
            IServiceProvider serviceProvider,
            IMediator mediator,
            UserAccessContext context)
        {
            _domainEventAccessor = domainEventAccessor;
            _serviceProvider = serviceProvider;
            _mediator = mediator;
            _context = context;
        }

        public async Task DispatchAsync()
        {
            var domainEvents = new Queue<IDomainEvent>(_domainEventAccessor.GetDomainEvents());
            
            while (domainEvents.Count > 0)
            {
                var domainEvent = domainEvents.Dequeue();

                await _mediator.Publish(domainEvent);
                
                AddDomainEventNotificationToOutbox(domainEvent);
                
                foreach (var newEvent in _domainEventAccessor.GetDomainEvents())
                {
                    domainEvents.Enqueue(newEvent);
                }
            }
        }
        
        private void AddDomainEventNotificationToOutbox(IDomainEvent domainEvent)
        {
            var domainEventType = domainEvent.GetType();
            var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEventType);
            var notificationHandlerType = typeof(INotificationHandler<>).MakeGenericType(notificationType);

            if (_serviceProvider.GetService(notificationHandlerType) is null)
            {
                return;
            }

            var constructor = notificationType.GetConstructor(BindingFlags.Instance | BindingFlags.Public,
                null, new Type[] {typeof(Guid), domainEventType}, Array.Empty<ParameterModifier>());

            var notification = (IDomainEventNotification<IDomainEvent>) constructor.Invoke(new object []
            {
                Guid.NewGuid(),
                domainEvent
            });
            
            var notificationData = JsonSerializer.Serialize(notification, notificationType);
            var outboxMessage = new OutboxMessage(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notificationType.FullName,
                notificationData);
            _context.OutboxMessages.Add(outboxMessage);
        }
    }
}