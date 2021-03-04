using Aimrank.Common.Domain;

namespace Aimrank.Domain.Friendships.Rules
{
    public class FriendshipMustHaveStateRule : IBusinessRule
    {
        private readonly Friendship _friendship;
        private readonly FriendshipState _state;

        public FriendshipMustHaveStateRule(Friendship friendship, FriendshipState state)
        {
            _friendship = friendship;
            _state = state;
        }

        public string Message => "Friendship does not have correct state";
        public string Code => "invalid_friendship_state";

        public bool IsBroken() => _friendship.GetState() != _state;
    }
}