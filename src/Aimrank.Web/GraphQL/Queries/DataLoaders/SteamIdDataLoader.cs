using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Application.Players.GetPlayerBatch;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public class SteamIdDataLoader : DataLoaderBase<Guid, string>
    {
        private readonly IMatchesModule _matchesModule;
        
        public SteamIdDataLoader(IBatchScheduler batchScheduler, IMatchesModule matchesModule)
            : base(batchScheduler)
        {
            _matchesModule = matchesModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<string>>> FetchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var result = await _matchesModule.ExecuteQueryAsync(new GetPlayerBatchQuery(keys));

            return result.Select(p => p is null
                ? Result<string>.Reject(null)
                : Result<string>.Resolve(p.SteamId)).ToList();
        }
    }
}