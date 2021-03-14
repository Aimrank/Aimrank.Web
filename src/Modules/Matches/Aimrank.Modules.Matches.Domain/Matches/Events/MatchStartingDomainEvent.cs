using Aimrank.Common.Domain;

namespace Aimrank.Modules.Matches.Domain.Matches.Events
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