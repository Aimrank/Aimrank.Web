using Aimrank.Common.Application.Events;
using System;

namespace Aimrank.Modules.Matches.IntegrationEvents
{
    public abstract class IntegrationEventBase : IIntegrationEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime OccurredAt { get; } = DateTime.UtcNow;
    }
}