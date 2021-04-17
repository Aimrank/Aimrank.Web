using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Application.Lobbies.GetLobbyMatch;
using Aimrank.Web.App.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.App.GraphQL.Queries.DataLoaders
{
    public class LobbyMatchDataLoader : DataLoaderBase<Guid, LobbyMatch>
    {
        private readonly IMatchesModule _matchesModule;
        
        public LobbyMatchDataLoader(IBatchScheduler batchScheduler, IMatchesModule matchesModule)
            : base(batchScheduler)
        {
            _matchesModule = matchesModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<LobbyMatch>>> FetchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var result = new List<Result<LobbyMatch>>();

            foreach (var lobbyId in keys)
            {
                var match = await _matchesModule.ExecuteQueryAsync(new GetLobbyMatchQuery(lobbyId));

                result.Add(match is null
                    ? Result<LobbyMatch>.Reject(null)
                    : Result<LobbyMatch>.Resolve(new LobbyMatch(match)));
            }

            return result;
        }
    }
}