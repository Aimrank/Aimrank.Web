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
        private readonly IMediator _mediator;
        private readonly UserAccessContext _context;

        public DomainEventDispatcher(IDomainEventAccessor domainEventAccessor, IMediator mediator, UserAccessContext context)
        {
            _domainEventAccessor = domainEventAccessor;
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
            var notification = (IDomainEventNotification<IDomainEvent>) Activator.CreateInstance(
                notificationType, BindingFlags.Instance | BindingFlags.Public, null, new object[]
                {
                    Guid.NewGuid(),
                    domainEvent
                });
            var notificationData = JsonSerializer.Serialize(notification, notificationType);
            var outboxMessage = new OutboxMessage(
                notification.Id,
                notification.DomainEvent.OccurredAt,
                notificationType.FullName,
                notificationData);
            _context.OutboxMessages.Add(outboxMessage);
        }
    }
}