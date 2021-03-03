using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Friendships;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Friendships.DeclineFriendshipInvitation
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