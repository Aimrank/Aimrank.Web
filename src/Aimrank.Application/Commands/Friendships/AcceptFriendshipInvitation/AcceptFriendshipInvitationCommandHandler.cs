using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Friendships;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Friendships.AcceptFriendshipInvitation
{
    internal class AcceptFriendshipInvitationCommandHandler : ICommandHandler<AcceptFriendshipInvitationCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IFriendshipRepository _friendshipRepository;

        public AcceptFriendshipInvitationCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            IFriendshipRepository friendshipRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _friendshipRepository = friendshipRepository;
        }

        public async Task<Unit> Handle(AcceptFriendshipInvitationCommand request, CancellationToken cancellationToken)
        {
            var invitingUserId = new UserId(request.InvitingUserId);
            var invitedUserId = new UserId(_executionContextAccessor.UserId);

            var members = new FriendshipMembers(invitingUserId, invitedUserId);

            var friendship = await _friendshipRepository.GetByMembersAsync(members);
            
            friendship.Accept(invitedUserId);
            
            _friendshipRepository.Update(friendship);
            
            return Unit.Value;
        }
    }
}