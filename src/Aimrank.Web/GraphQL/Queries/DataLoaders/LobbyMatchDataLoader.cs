using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Lobbies.GetLobbyMatch;
using Aimrank.Web.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public class LobbyMatchDataLoader : DataLoaderBase<Guid, LobbyMatch>
    {
        private readonly IAimrankModule _aimrankModule;
        
        public LobbyMatchDataLoader(IBatchScheduler batchScheduler, IAimrankModule aimrankModule)
            : base(batchScheduler)
        {
            _aimrankModule = aimrankModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<LobbyMatch>>> FetchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var result = new List<Result<LobbyMatch>>();

            foreach (var lobbyId in keys)
            {
                var match = await _aimrankModule.ExecuteQueryAsync(new GetLobbyMatchQuery(lobbyId));

                result.Add(match is null
                    ? Result<LobbyMatch>.Reject(null)
                    : Result<LobbyMatch>.Resolve(new LobbyMatch(match)));
            }

            return result;
        }
    }
}