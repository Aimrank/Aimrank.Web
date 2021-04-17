using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.UserAccess.Domain.Friendships.Rules;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.UserAccess.Domain.Friendships
{
    public class Friendship : Entity, IAggregateRoot
    {
        public UserId User1Id { get; private init; }
        public UserId User2Id { get; private init; }
        private readonly UserId _invitingUserId;
        private UserId _blockingUserId1;
        private UserId _blockingUserId2;
        private DateTime _createdAt;
        private bool _isAccepted;
        
        public FriendshipMembers Members
        {
            get => new(User1Id, User2Id);
            private init
            {
                User1Id = value.UserId1;
                User2Id = value.UserId2;
            }
        }

        private ImmutableHashSet<UserId> BlockingUsers
        {
            get => new[] {_blockingUserId1, _blockingUserId2}.Where(id => id is not null).ToImmutableHashSet();
            set
            {
                _blockingUserId1 = null;
                _blockingUserId2 = null;
                
                if (value.Count > 0)
                {
                    _blockingUserId1 = value.ElementAt(0);
                }

                if (value.Count > 1)
                {
                    _blockingUserId2 = value.ElementAt(1);
                }
            }
        }

        private Friendship() {}

        private Friendship(FriendshipMembers members, UserId invitingUserId = null, UserId blockingUserId = null)
        {
            Members = members;
            _invitingUserId = invitingUserId;
            _blockingUserId1 = blockingUserId;
            _createdAt = DateTime.UtcNow;
        }

        public static async Task<Friendship> CreateAsync(
            FriendshipMembers members,
            IFriendshipRepository friendshipRepository,
            UserId invitingUserId = null,
            UserId blockingUserId = null)
        {
            await BusinessRules.CheckAsync(new FriendshipMustBeUniqueRule(members, friendshipRepository));

            BusinessRules.Check(new FriendshipMustHaveInvitingUserOrBlockingUserRule(invitingUserId, blockingUserId));

            var friendship = new Friendship(members, invitingUserId, blockingUserId);

            return friendship;
        }

        public void Accept(UserId acceptingUserId)
        {
            BusinessRules.Check(new FriendshipMustHaveStateRule(this, FriendshipState.Pending));
            BusinessRules.Check(new FriendshipCanBeAcceptedOnlyByInvitedUserRule(GetInvitedUserId(), acceptingUserId));

            _isAccepted = true;
        }

        public void Decline(UserId decliningUserId, IFriendshipRepository friendshipRepository)
        {
            BusinessRules.Check(new FriendshipMustHaveStateRule(this, FriendshipState.Pending));
            BusinessRules.Check(new FriendshipCanBeDeclinedOnlyByInvitedUserRule(GetInvitedUserId(), decliningUserId));
            
            friendshipRepository.Delete(this);
        }

        public void Block(UserId blockingUserId)
        {
            BusinessRules.Check(new UserMustBeFriendshipMemberRule(Members, blockingUserId));
            BusinessRules.Check(new FriendshipMustNotBeBlockedByUserRule(BlockingUsers, blockingUserId));

            BlockingUsers = BlockingUsers.Add(blockingUserId);
        }

        public void Unblock(UserId unblockingUserId, IFriendshipRepository friendshipRepository)
        {
            BusinessRules.Check(new FriendshipMustBeBlockedByUserRule(BlockingUsers, unblockingUserId));

            BlockingUsers = BlockingUsers.Remove(unblockingUserId);

            if (BlockingUsers.Count == 0 && !_isAccepted)
            {
                friendshipRepository.Delete(this);
            }
        }

        public void Delete(UserId deletingUserId, IFriendshipRepository friendshipRepository)
        {
            BusinessRules.Check(new UserMustBeFriendshipMemberRule(Members, deletingUserId));
            
            if (GetState() != FriendshipState.Blocked)
            {
                friendshipRepository.Delete(this);
            }
            else
            {
                if (BlockingUsers.Count == 1 && BlockingUsers.Contains(deletingUserId))
                {
                    friendshipRepository.Delete(this);
                }
                else
                {
                    _isAccepted = false;
                    
                    BlockingUsers = BlockingUsers.Remove(deletingUserId);
                }
            }
        }

        public FriendshipState GetState()
        {
            if (BlockingUsers.Count > 0)
            {
                return FriendshipState.Blocked;
            }

            return _isAccepted ? FriendshipState.Active : FriendshipState.Pending;
        }

        private UserId GetInvitedUserId() => _invitingUserId == Members.UserId1 ? Members.UserId2 : Members.UserId1;
    }
}