using Aimrank.Modules.UserAccess.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Modules.UserAccess.Application.Users.GetUserBatch
{
    public class GetUserBatchQuery : IQuery<IEnumerable<UserDto>>
    {
        public IEnumerable<Guid> UserIds { get; }

        public GetUserBatchQuery(IEnumerable<Guid> userIds)
        {
            UserIds = userIds;
        }
    }
}