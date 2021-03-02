using Aimrank.Common.Domain;
using System.Threading.Tasks;

namespace Aimrank.Domain.Friendships.Rules
{
    public class FriendshipMustBeUniqueRule : IAsyncBusinessRule
    {
        private readonly FriendshipMembers _members;
        private readonly IFriendshipRepository _friendshipRepository;

        public FriendshipMustBeUniqueRule(FriendshipMembers members, IFriendshipRepository friendshipRepository)
        {
            _members = members;
            _friendshipRepository = friendshipRepository;
        }

        public string Message => "Friendship already exists";
        public string Code => "friendship_already_exists";

        public Task<bool> IsBrokenAsync() => _friendshipRepository.ExistsForMembersAsync(_members);
    }
}