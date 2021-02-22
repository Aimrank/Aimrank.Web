using System;

namespace Aimrank.Application.Queries.Matches.GetMatchesHistory
{
    public class MatchHistoryPlayerDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int Team { get; set; }
        public int Kills { get; set; }
        public int Assists { get; set; }
        public int Deaths { get; set; }
        public int Score { get; set; }
        public int RatingStart { get; set; }
        public int RatingEnd { get; set; }
    }
}