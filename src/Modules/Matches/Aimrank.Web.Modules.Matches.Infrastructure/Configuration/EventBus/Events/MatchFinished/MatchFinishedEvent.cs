using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Application.Matches.FinishMatch;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus.Events.MatchFinished
{
    [IntegrationEvent("Aimrank.Pod")]
    internal class MatchFinishedEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; }
        public Guid MatchId { get; set; }
        public int Winner { get; set; }
        public FinishMatchCommand.MatchEndEventTeam TeamTerrorists { get; set; }
        public FinishMatchCommand.MatchEndEventTeam TeamCounterTerrorists { get; set; }
    }
}