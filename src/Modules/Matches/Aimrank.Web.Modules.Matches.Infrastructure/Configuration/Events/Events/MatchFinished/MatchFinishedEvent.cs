using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Application.Matches.FinishMatch;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Events.Events.MatchFinished
{
    [IntegrationEvent("Aimrank.Pod")]
    internal class MatchFinishedEvent : IIntegrationEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
        public Guid MatchId { get; }
        public int Winner { get; }
        public FinishMatchCommand.MatchEndEventTeam TeamTerrorists { get; }
        public FinishMatchCommand.MatchEndEventTeam TeamCounterTerrorists { get; }

        public MatchFinishedEvent(
            Guid id,
            DateTime occurredOn,
            Guid matchId,
            int winner,
            FinishMatchCommand.MatchEndEventTeam teamTerrorists,
            FinishMatchCommand.MatchEndEventTeam teamCounterTerrorists)
        {
            Id = id;
            OccurredOn = occurredOn;
            MatchId = matchId;
            Winner = winner;
            TeamTerrorists = teamTerrorists;
            TeamCounterTerrorists = teamCounterTerrorists;
        }
    }
}