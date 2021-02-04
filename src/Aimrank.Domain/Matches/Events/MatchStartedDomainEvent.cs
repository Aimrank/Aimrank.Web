using Aimrank.Common.Domain;

namespace Aimrank.Domain.Matches.Events
{
    public class MatchStartedDomainEvent : IDomainEvent
    {
        public Match Match { get; }

        public MatchStartedDomainEvent(Match match)
        {
            Match = match;
        }
    }
}