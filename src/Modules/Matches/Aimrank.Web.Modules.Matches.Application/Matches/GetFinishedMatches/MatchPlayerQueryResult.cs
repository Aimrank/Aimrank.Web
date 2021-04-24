using System;

namespace Aimrank.Web.Modules.Matches.Application.Matches.GetFinishedMatches
{
    internal class MatchPlayerQueryResult
    {
		public Guid Player_Id { get; set; }
		public int Player_Team { get; set; }
		public int Player_Kills { get; set; }
		public int Player_Assists { get; set; }
		public int Player_Deaths { get; set; }
		public int Player_Hs { get; set; }
		public int Player_Rating_Start { get; set; }
		public int Player_Rating_End { get; set; }
    }
}