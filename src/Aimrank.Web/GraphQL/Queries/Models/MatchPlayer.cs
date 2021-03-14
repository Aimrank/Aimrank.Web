using Aimrank.Modules.Matches.Application.Matches.GetFinishedMatches;
using Aimrank.Web.GraphQL.Queries.DataLoaders;
using HotChocolate;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.Models
{
    public class MatchPlayer
    {
        private readonly Guid _userId;
        
        public int Team { get; }
        public int Kills { get; }
        public int Assists { get; }
        public int Deaths { get; }
        public int Hs { get; }
        public int RatingStart { get; }
        public int RatingEnd { get; }

        public MatchPlayer(MatchPlayerDto dto)
        {
            _userId = dto.Id;
            Team = dto.Team;
            Kills = dto.Kills;
            Assists = dto.Assists;
            Deaths = dto.Deaths;
            Hs = dto.Hs;
            RatingStart = dto.RatingStart;
            RatingEnd = dto.RatingEnd;
        }

        public Task<User> GetUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_userId, CancellationToken.None);
    }
}