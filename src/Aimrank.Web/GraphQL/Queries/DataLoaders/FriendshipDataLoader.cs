using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Friendships.GetFriendshipBatch;
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
        private readonly IAimrankModule _aimrankModule;
        
        public FriendshipDataLoader(IBatchScheduler batchScheduler, IAimrankModule aimrankModule)
            : base(batchScheduler)
        {
            _aimrankModule = aimrankModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<Friendship>>> FetchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var result = await _aimrankModule.ExecuteQueryAsync(new GetFriendshipBatchQuery(keys));
            
            return result.Select(f => f is null
                ? Result<Friendship>.Reject(null)
                : Result<Friendship>.Resolve(new Friendship(f))).ToList();
        }
    }
}