using System;

namespace Aimrank.Application.Queries.Matches.GetMatchesHistory
{
    internal class MatchHistoryPlayerQueryResult
    {
		public Guid User_Id { get; set; }
		public string User_Username { get; set; }
		public int User_Team { get; set; }
		public int User_Kills { get; set; }
		public int User_Assists { get; set; }
		public int User_Deaths { get; set; }
		public int User_Score { get; set; }
		public int User_RatingStart { get; set; }
		public int User_RatingEnd { get; set; }
    }
}