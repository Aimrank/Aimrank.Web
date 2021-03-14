using Aimrank.Common.Domain;

namespace Aimrank.Modules.Matches.Domain.Matches.Events
{
    public class MatchPlayerLeftDomainEvent : IDomainEvent
    {
        public MatchPlayer Player { get; }

        public MatchPlayerLeftDomainEvent(MatchPlayer player)
        {
            Player = player;
        }
    }
}