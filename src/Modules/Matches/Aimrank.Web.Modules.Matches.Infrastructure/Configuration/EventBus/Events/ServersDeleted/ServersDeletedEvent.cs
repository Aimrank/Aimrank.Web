using Aimrank.Web.Common.Application.Events;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus.Events.ServersDeleted
{
    [IntegrationEvent("Aimrank.Cluster")]
    internal class ServersDeletedEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; }
        public IEnumerable<Guid> Ids { get; set; }
    }
}