using Aimrank.Web.Common.Domain;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
{
    public class MatchFinishedDomainEvent : DomainEvent
    {
        public MatchId MatchId { get; }
        public int ScoreT { get; }
        public int ScoreCT { get; }
        public IEnumerable<MatchLobby> Lobbies { get; }

        public MatchFinishedDomainEvent(MatchId matchId, int scoreT, int scoreCt, IEnumerable<MatchLobby> lobbies)
        {
            MatchId = matchId;
            ScoreT = scoreT;
            ScoreCT = scoreCt;
            Lobbies = lobbies;
        }
    }
}