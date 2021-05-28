using Aimrank.Web.Common.Application.Events;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Events.Events.MatchCanceled
{
    [IntegrationEvent("Aimrank.Agones")]
    internal class MatchCanceledEvent : IIntegrationEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
        public Guid MatchId { get; }

        public MatchCanceledEvent(Guid id, DateTime occurredOn, Guid matchId)
        {
            Id = id;
            OccurredOn = occurredOn;
            MatchId = matchId;
        }
    }
}