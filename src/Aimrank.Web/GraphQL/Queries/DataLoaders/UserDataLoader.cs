using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserBatch;
using Aimrank.Web.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public class UserDataLoader : DataLoaderBase<Guid, User>
    {
        private readonly IAimrankModule _aimrankModule;
        
        public UserDataLoader(IBatchScheduler batchScheduler, IAimrankModule aimrankModule)
            : base(batchScheduler)
        {
            _aimrankModule = aimrankModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<User>>> FetchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var result = await _aimrankModule.ExecuteQueryAsync(new GetUserBatchQuery(keys));

            return result.Select(u => u is null
                ? Result<User>.Reject(null)
                : Result<User>.Resolve(new User(u))).ToList();
        }
    }
}