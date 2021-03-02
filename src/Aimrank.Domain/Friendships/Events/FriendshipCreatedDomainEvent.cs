using Aimrank.Common.Domain;

namespace Aimrank.Domain.Friendships.Events
{
    public class FriendshipCreatedDomainEvent : IDomainEvent
    {
        public Friendship Friendship { get; }

        public FriendshipCreatedDomainEvent(Friendship friendship)
        {
            Friendship = friendship;
        }
    }
}