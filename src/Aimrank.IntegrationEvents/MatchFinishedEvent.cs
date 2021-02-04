using Aimrank.Common.Application.Events;
using System;

namespace Aimrank.IntegrationEvents
{
    public class MatchFinishedEvent : IntegrationEvent
    {
        public Guid MatchId { get; }
        public int ScoreT { get; }
        public int ScoreCT { get; }

        public MatchFinishedEvent(
            Guid id,
            Guid matchId,
            int scoreT,
            int scoreCt,
            DateTime occurredAt)
            : base(id, occurredAt)
        {
            MatchId = matchId;
            ScoreT = scoreT;
            ScoreCT = scoreCt;
        }
    }
}