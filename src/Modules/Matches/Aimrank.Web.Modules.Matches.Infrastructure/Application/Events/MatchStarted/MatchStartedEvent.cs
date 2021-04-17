using Aimrank.Web.Common.Application.Events;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Application.Events.MatchStarted
{
    [IntegrationEvent("Aimrank.Pod")]
    internal class MatchStartedEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredAt { get; set; }
        public Guid MatchId { get; set; }
    }
}