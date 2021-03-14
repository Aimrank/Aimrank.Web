using Aimrank.Common.Domain;
using System.Collections.Generic;

namespace Aimrank.Modules.Matches.Domain.Matches
{
    public class MatchPlayerStats : ValueObject
    {
        public int Kills { get; }
        public int Assists { get; }
        public int Deaths { get; }
        public int Hs { get; }

        public MatchPlayerStats(int kills, int assists, int deaths, int hs)
        {
            Kills = kills;
            Assists = assists;
            Deaths = deaths;
            Hs = hs;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Kills;
            yield return Assists;
            yield return Deaths;
            yield return Hs;
        }
    }
}