using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Friendships.GetFriendshipBatch;
using Aimrank.Web.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public class FriendshipDataLoader : DataLoaderBase<Guid, Friendship>
    {
        private readonly IUserAccessModule _userAccessModule;
        
        public FriendshipDataLoader(IBatchScheduler batchScheduler, IUserAccessModule userAccessModule)
            : base(batchScheduler)
        {
            _userAccessModule = userAccessModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<Friendship>>> FetchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var result = await _userAccessModule.ExecuteQueryAsync(new GetFriendshipBatchQuery(keys));
            
            return result.Select(f => f is null
                ? Result<Friendship>.Reject(null)
                : Result<Friendship>.Resolve(new Friendship(f))).ToList();
        }
    }
}