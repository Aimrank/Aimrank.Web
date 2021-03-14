using System;

namespace Aimrank.Modules.Matches.Application.Matches.GetFinishedMatches
{
    internal class MatchPlayerQueryResult
    {
		public Guid User_Id { get; set; }
		public int User_Team { get; set; }
		public int User_Kills { get; set; }
		public int User_Assists { get; set; }
		public int User_Deaths { get; set; }
		public int User_Hs { get; set; }
		public int User_RatingStart { get; set; }
		public int User_RatingEnd { get; set; }
    }
}