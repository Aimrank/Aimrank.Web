using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Users.GetUserStatsBatch;
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
        private readonly IUserAccessModule _userAccessModule;
        
        public UserStatsDataLoader(IBatchScheduler batchScheduler, IUserAccessModule userAccessModule)
            : base(batchScheduler)
        {
            _userAccessModule = userAccessModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<UserStatsDto>>> FetchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var result = await _userAccessModule.ExecuteQueryAsync(new GetUserStatsBatchQuery(keys));

            return result.Select(u => u is null
                ? Result<UserStatsDto>.Reject(null)
                : Result<UserStatsDto>.Resolve(u)).ToList();
        }
    }
}