using Aimrank.Web.Common.Application.Events;
using System;

namespace Aimrank.Web.Modules.Cluster.IntegrationEvents
{
    public abstract class IntegrationEventBase : IIntegrationEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}