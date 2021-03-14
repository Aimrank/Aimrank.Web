using System;

namespace Aimrank.Modules.Matches.Application.Matches.GetFinishedMatches
{
    public class MatchPlayerDto
    {
        public Guid Id { get; set; }
        public int Team { get; set; }
        public int Kills { get; set; }
        public int Assists { get; set; }
        public int Deaths { get; set; }
        public int Hs { get; set; }
        public int RatingStart { get; set; }
        public int RatingEnd { get; set; }
    }
}