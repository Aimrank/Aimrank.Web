using Aimrank.Common.Domain;

namespace Aimrank.Domain.Matches.Events
{
    public class MatchStatusChangedDomainEvent : IDomainEvent
    {
        public Match Match { get; }

        public MatchStatusChangedDomainEvent(Match match)
        {
            Match = match;
        }
    }
}