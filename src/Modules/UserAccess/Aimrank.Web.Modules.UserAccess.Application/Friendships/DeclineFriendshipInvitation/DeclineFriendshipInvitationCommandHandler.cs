using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Domain.Friendships;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.DeclineFriendshipInvitation
{
    internal class DeclineFriendshipInvitationCommandHandler : ICommandHandler<DeclineFriendshipInvitationCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IFriendshipRepository _friendshipRepository;

        public DeclineFriendshipInvitationCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            IFriendshipRepository friendshipRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _friendshipRepository = friendshipRepository;
        }

        public async Task<Unit> Handle(DeclineFriendshipInvitationCommand request, CancellationToken cancellationToken)
        {
            var invitingUserId = new UserId(request.InvitingUserId);
            var invitedUserId = new UserId(_executionContextAccessor.UserId);

            var members = new FriendshipMembers(invitingUserId, invitedUserId);

            var friendship = await _friendshipRepository.GetByMembersAsync(members);
            
            friendship.Decline(invitedUserId, _friendshipRepository);
            
            return Unit.Value;
        }
    }
}