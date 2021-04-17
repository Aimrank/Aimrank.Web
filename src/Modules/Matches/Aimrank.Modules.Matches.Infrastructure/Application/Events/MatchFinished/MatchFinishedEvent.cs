using Aimrank.Common.Application.Events;
using Aimrank.Modules.Matches.Application.Matches.FinishMatch;
using System;

namespace Aimrank.Modules.Matches.Infrastructure.Application.Events.MatchFinished
{
    [IntegrationEvent("Aimrank.Pod")]
    internal class MatchFinishedEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredAt { get; set; }
        public Guid MatchId { get; set; }
        public int Winner { get; set; }
        public FinishMatchCommand.MatchEndEventTeam TeamTerrorists { get; set; }
        public FinishMatchCommand.MatchEndEventTeam TeamCounterTerrorists { get; set; }
    }
}