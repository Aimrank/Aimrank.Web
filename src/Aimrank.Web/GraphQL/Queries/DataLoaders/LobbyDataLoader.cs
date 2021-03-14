using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Application.Lobbies.GetLobbyForUser;
using Aimrank.Web.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public class LobbyDataLoader : DataLoaderBase<int, Lobby>
    {
        private readonly IMatchesModule _matchesModule;
        
        public LobbyDataLoader(IBatchScheduler batchScheduler, IMatchesModule matchesModule)
            : base(batchScheduler)
        {
            _matchesModule = matchesModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<Lobby>>> FetchAsync(IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            var result = await _matchesModule.ExecuteQueryAsync(new GetLobbyForUserQuery());

            return keys.Select(_ => result is null
                ? Result<Lobby>.Reject(null)
                : Result<Lobby>.Resolve(new Lobby(result))).ToList();
        }
    }
}