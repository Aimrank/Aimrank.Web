using Aimrank.Web.Modules.Matches.Application.Matches.GetFinishedMatches;
using Aimrank.Web.App.GraphQL.Queries.DataLoaders;
using HotChocolate.Types;
using HotChocolate;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.App.GraphQL.Queries.Models
{
    public class MatchPlayer
    {
        private readonly Guid _playerId;
        
        public int Team { get; }
        public int Kills { get; }
        public int Assists { get; }
        public int Deaths { get; }
        public int Hs { get; }
        public int RatingStart { get; }
        public int RatingEnd { get; }

        public MatchPlayer(MatchPlayerDto dto)
        {
            _playerId = dto.Id;
            Team = dto.Team;
            Kills = dto.Kills;
            Assists = dto.Assists;
            Deaths = dto.Deaths;
            Hs = dto.Hs;
            RatingStart = dto.RatingStart;
            RatingEnd = dto.RatingEnd;
        }

        public Task<User> GetUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_playerId, CancellationToken.None);
    }

    public class MatchPlayerType : ObjectType<MatchPlayer>
    {
    }
}