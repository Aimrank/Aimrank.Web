using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.GetFriendshipBatch
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