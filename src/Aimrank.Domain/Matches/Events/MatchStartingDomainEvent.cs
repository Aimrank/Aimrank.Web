using Aimrank.Common.Domain;

namespace Aimrank.Domain.Matches.Events
{
    public class MatchStartingDomainEvent : IDomainEvent
    {
        public Match Match { get; }

        public MatchStartingDomainEvent(Match match)
        {
            Match = match;
        }
    }
}