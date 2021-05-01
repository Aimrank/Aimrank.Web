using Aimrank.Web.Common.Application.Events;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus.Events.MatchStarted
{
    [IntegrationEvent("Aimrank.Pod")]
    internal class MatchStartedEvent : IIntegrationEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
        public Guid MatchId { get; }

        public MatchStartedEvent(Guid id, DateTime occurredOn, Guid matchId)
        {
            Id = id;
            OccurredOn = occurredOn;
            MatchId = matchId;
        }
    }
}