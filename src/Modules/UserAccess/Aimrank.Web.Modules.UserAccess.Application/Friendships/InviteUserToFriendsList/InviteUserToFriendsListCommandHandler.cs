using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Domain.Friendships;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.InviteUserToFriendsList
{
    internal class InviteUserToFriendsListCommandHandler : ICommandHandler<InviteUserToFriendsListCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IFriendshipRepository _friendshipRepository;

        public InviteUserToFriendsListCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            IFriendshipRepository friendshipRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _friendshipRepository = friendshipRepository;
        }

        public async Task<Unit> Handle(InviteUserToFriendsListCommand request, CancellationToken cancellationToken)
        {
            var invitingUserId = new UserId(_executionContextAccessor.UserId);
            var invitedUserId = new UserId(request.InvitedUserId);

            var members = new FriendshipMembers(invitingUserId, invitedUserId);

            var friendship = await Friendship.CreateAsync(members, _friendshipRepository, invitingUserId);
            
            _friendshipRepository.Add(friendship);
            
            return Unit.Value;
        }
    }
}