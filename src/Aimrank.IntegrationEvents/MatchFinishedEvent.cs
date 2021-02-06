using System;

namespace Aimrank.IntegrationEvents
{
    public class MatchFinishedEvent : IntegrationEventBase
    {
        public Guid MatchId { get; }
        public int ScoreT { get; }
        public int ScoreCT { get; }

        public MatchFinishedEvent(
            Guid matchId,
            int scoreT,
            int scoreCt)
        {
            MatchId = matchId;
            ScoreT = scoreT;
            ScoreCT = scoreCt;
        }
    }
}