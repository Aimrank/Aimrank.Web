using System.Collections.Generic;
using System;

namespace Aimrank.Modules.Matches.IntegrationEvents.Matches
{
    public class MatchFinishedEvent : IntegrationEventBase
    {
        public Guid MatchId { get; }
        public int ScoreT { get; }
        public int ScoreCT { get; }
        public IEnumerable<Guid> Lobbies { get; }

        public MatchFinishedEvent(Guid matchId, int scoreT, int scoreCt, IEnumerable<Guid> lobbies)
        {
            MatchId = matchId;
            ScoreT = scoreT;
            ScoreCT = scoreCt;
            Lobbies = lobbies;
        }
    }
}