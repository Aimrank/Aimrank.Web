using Aimrank.Web.Common.Domain;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
{
    public class MatchFinishedDomainEvent : DomainEvent
    {
        public Match Match { get; }
        public int ScoreT { get; }
        public int ScoreCT { get; }
        public IEnumerable<MatchLobby> Lobbies { get; }

        public MatchFinishedDomainEvent(Match match, int scoreT, int scoreCt, IEnumerable<MatchLobby> lobbies)
        {
            Match = match;
            ScoreT = scoreT;
            ScoreCT = scoreCt;
            Lobbies = lobbies;
        }
    }
}