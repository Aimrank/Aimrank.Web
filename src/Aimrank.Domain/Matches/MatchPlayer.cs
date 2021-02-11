using Aimrank.Common.Domain;
using Aimrank.Domain.Users;

namespace Aimrank.Domain.Matches
{
    public class MatchPlayer : Entity
    {
        public UserId UserId { get; }
        public string SteamId { get; }
        public MatchTeam Team { get; }
        public MatchPlayerStats Stats { get; private set; }
        
        private MatchPlayer() {}

        internal MatchPlayer(UserId userId, string steamId, MatchTeam team)
        {
            UserId = userId;
            SteamId = steamId;
            Team = team;
        }

        internal void UpdateStats(MatchPlayerStats stats) => Stats = stats;
    }
}