using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Lobbies.GetLobbyForUser;
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
        private readonly IAimrankModule _aimrankModule;
        
        public LobbyDataLoader(IBatchScheduler batchScheduler, IAimrankModule aimrankModule)
            : base(batchScheduler)
        {
            _aimrankModule = aimrankModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<Lobby>>> FetchAsync(IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            var result = await _aimrankModule.ExecuteQueryAsync(new GetLobbyForUserQuery());

            return keys.Select(_ => result is null
                ? Result<Lobby>.Reject(null)
                : Result<Lobby>.Resolve(new Lobby(result))).ToList();
        }
    }
}