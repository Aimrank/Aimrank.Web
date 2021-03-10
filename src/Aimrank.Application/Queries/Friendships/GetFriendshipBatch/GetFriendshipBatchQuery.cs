using Aimrank.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.Friendships.GetFriendshipBatch
{
    public class GetFriendshipBatchQuery : IQuery<IEnumerable<FriendshipDto>>
    {
        public IEnumerable<Guid> UserIds { get; }

        public GetFriendshipBatchQuery(IEnumerable<Guid> userIds)
        {
            UserIds = userIds;
        }
    }
}