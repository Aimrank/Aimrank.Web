using Aimrank.Web.Common.Application.Events;
using System;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.EventBus.Events.MatchCanceled
{
    [IntegrationEvent("Aimrank.Pod")]
    internal class MatchCanceledEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredAt { get; set; }
        public Guid MatchId { get; set; }
    }
}