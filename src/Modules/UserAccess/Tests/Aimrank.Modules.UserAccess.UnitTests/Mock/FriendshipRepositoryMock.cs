using Aimrank.Common.Application.Exceptions;
using Aimrank.Modules.UserAccess.Domain.Friendships;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Modules.UserAccess.UnitTests.Mock
{
    internal class FriendshipRepositoryMock : IFriendshipRepository
    {
        private readonly Dictionary<FriendshipMembers, Friendship> _friendships = new();

        public Task<Friendship> GetByMembersAsync(FriendshipMembers members)
        {
            var friendship = _friendships.GetValueOrDefault(members);
            if (friendship is null)
            {
                throw new EntityNotFoundException();
            }

            return Task.FromResult(friendship);
        }

        public Task<bool> ExistsForMembersAsync(FriendshipMembers members)
            => Task.FromResult(_friendships.ContainsKey(members));

        public void Add(Friendship friendship) => _friendships.Add(friendship.Members, friendship);

        public void Delete(Friendship friendship) => _friendships.Remove(friendship.Members);
    }
}