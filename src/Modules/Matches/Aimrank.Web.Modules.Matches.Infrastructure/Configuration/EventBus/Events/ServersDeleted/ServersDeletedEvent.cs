using Aimrank.Web.Common.Application.Events;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus.Events.ServersDeleted
{
    [IntegrationEvent("Aimrank.Cluster")]
    internal class ServersDeletedEvent : IIntegrationEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
        public IEnumerable<Guid> Ids { get; }

        public ServersDeletedEvent(Guid id, DateTime occurredOn, IEnumerable<Guid> ids)
        {
            Id = id;
            OccurredOn = occurredOn;
            Ids = ids;
        }
    }
}