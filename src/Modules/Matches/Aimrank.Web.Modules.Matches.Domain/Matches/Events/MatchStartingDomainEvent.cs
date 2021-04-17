using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.Matches.Domain.Matches.Events
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