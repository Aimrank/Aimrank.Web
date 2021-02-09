using System.Collections.Generic;

namespace Aimrank.Domain.Matches
{
    public record MatchTeam
    {
        private readonly HashSet<MatchPlayer> _players = new();
        
        public string Name { get; }
        public int Score { get; }

        public IEnumerable<MatchPlayer> Players
        {
            get => _players;
            private init => _players = new HashSet<MatchPlayer>(value);
        }

        public MatchTeam(string name, int score, IEnumerable<MatchPlayer> players)
        {
            Name = name;
            Score = score;
            Players = players;
        }
    }
}