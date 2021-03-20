using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Application.Players.GetPlayerStatsBatch;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public class PlayerStatsDataLoader : DataLoaderBase<Guid, PlayerStatsDto>
    {
        private readonly IMatchesModule _matchesModule;
        
        public PlayerStatsDataLoader(IBatchScheduler batchScheduler, IMatchesModule matchesModule)
            : base(batchScheduler)
        {
            _matchesModule = matchesModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<PlayerStatsDto>>> FetchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var result = await _matchesModule.ExecuteQueryAsync(new GetPlayerStatsBatchQuery(keys));

            return result.Select(u => u is null
                ? Result<PlayerStatsDto>.Reject(null)
                : Result<PlayerStatsDto>.Resolve(u)).ToList();
        }
    }
}