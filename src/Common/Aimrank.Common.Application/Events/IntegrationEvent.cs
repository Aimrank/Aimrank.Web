using System;

namespace Aimrank.Common.Application.Events
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; }
        public DateTime OccurredAt { get; }

        protected IntegrationEvent(Guid id, DateTime occurredAt)
        {
            Id = id;
            OccurredAt = occurredAt;
        }
    }
}