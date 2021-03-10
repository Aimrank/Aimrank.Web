using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserStatsBatch;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public class UserStatsDataLoader : DataLoaderBase<Guid, UserStatsDto>
    {
        private readonly IAimrankModule _aimrankModule;
        
        public UserStatsDataLoader(IBatchScheduler batchScheduler, IAimrankModule aimrankModule)
            : base(batchScheduler)
        {
            _aimrankModule = aimrankModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<UserStatsDto>>> FetchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var result = await _aimrankModule.ExecuteQueryAsync(new GetUserStatsBatchQuery(keys));

            return result.Select(u => u is null
                ? Result<UserStatsDto>.Reject(null)
                : Result<UserStatsDto>.Resolve(u)).ToList();
        }
    }
}