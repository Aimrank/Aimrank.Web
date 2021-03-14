using Aimrank.Modules.UserAccess.Application.Friendships.GetFriendshipBatch;
using Aimrank.Web.GraphQL.Queries.DataLoaders;
using HotChocolate;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.Models
{
    public class Friendship
    {
        private readonly Guid _userId1;
        private readonly Guid _userId2;
        
        public Guid? InvitingUserId { get; }
        public bool IsAccepted { get; }
        public IEnumerable<Guid> BlockingUsersIds { get; }

        public Friendship(FriendshipDto dto)
        {
            _userId1 = dto.UserId1;
            _userId2 = dto.UserId2;
            InvitingUserId = dto.InvitingUserId;
            IsAccepted = dto.IsAccepted;
            BlockingUsersIds = dto.BlockingUsersIds;
        }

        public Task<User> GetUser1([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_userId1, CancellationToken.None);
        
        public Task<User> GetUser2([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_userId2, CancellationToken.None);
    }
}