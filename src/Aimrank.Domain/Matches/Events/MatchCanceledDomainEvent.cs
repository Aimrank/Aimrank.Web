using Aimrank.Common.Domain;

namespace Aimrank.Domain.Matches.Events
{
    public class MatchCanceledDomainEvent : IDomainEvent
    {
        public Match Match { get; }

        public MatchCanceledDomainEvent(Match match)
        {
            Match = match;
        }
    }
}