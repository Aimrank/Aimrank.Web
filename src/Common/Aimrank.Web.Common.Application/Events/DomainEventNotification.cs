using Aimrank.Web.Common.Domain;
using System;

namespace Aimrank.Web.Common.Application.Events
{
    public class DomainEventNotification<TEvent> : IDomainEventNotification<TEvent> 
        where TEvent : IDomainEvent
    {
        public Guid Id { get; }
        public TEvent DomainEvent { get; }

        public DomainEventNotification(Guid id, TEvent domainEvent)
        {
            Id = id;
            DomainEvent = domainEvent;
        }
    }
}