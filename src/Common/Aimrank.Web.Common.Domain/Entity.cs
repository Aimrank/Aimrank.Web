using System.Collections.Generic;

namespace Aimrank.Web.Common.Domain
{
    public abstract class Entity
    {
        private readonly Queue<IDomainEvent> _domainEvents = new();

        public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

        public void ClearDomainEvents() => _domainEvents.Clear();

        public void AddDomainEvent(IDomainEvent @event) => _domainEvents.Enqueue(@event);
    }
}