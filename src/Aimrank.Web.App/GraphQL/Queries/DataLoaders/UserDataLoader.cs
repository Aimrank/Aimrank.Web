using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Users.GetUserBatch;
using Aimrank.Web.App.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.App.GraphQL.Queries.DataLoaders
{
    public class UserDataLoader : DataLoaderBase<Guid, User>
    {
        private readonly IUserAccessModule _userAccessModule;
        
        public UserDataLoader(IBatchScheduler batchScheduler, IUserAccessModule userAccessModule)
            : base(batchScheduler)
        {
            _userAccessModule = userAccessModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<User>>> FetchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var result = await _userAccessModule.ExecuteQueryAsync(new GetUserBatchQuery(keys));

            return result.Select(u => u is null
                ? Result<User>.Reject(null)
                : Result<User>.Resolve(new User(u))).ToList();
        }
    }
}