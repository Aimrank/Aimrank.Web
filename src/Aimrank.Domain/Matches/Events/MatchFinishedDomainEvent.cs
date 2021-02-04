using Aimrank.Common.Domain;

namespace Aimrank.Domain.Matches.Events
{
    public class MatchFinishedDomainEvent : IDomainEvent
    {
        public Match Match { get; }

        public MatchFinishedDomainEvent(Match match)
        {
            Match = match;
        }
    }
}