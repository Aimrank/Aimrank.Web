using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Lobbies;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
{
    public class MatchFinishedDomainEvent : IDomainEvent
    {
        public Match Match { get; }
        public int ScoreT { get; }
        public int ScoreCT { get; }
        public IEnumerable<LobbyId> Lobbies { get; }

        public MatchFinishedDomainEvent(Match match, int scoreT, int scoreCt, IEnumerable<LobbyId> lobbies)
        {
            Match = match;
            ScoreT = scoreT;
            ScoreCT = scoreCt;
            Lobbies = lobbies;
        }
    }
}