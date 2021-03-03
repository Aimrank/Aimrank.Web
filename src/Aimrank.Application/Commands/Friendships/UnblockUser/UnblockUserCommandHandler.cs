using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Friendships;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Friendships.UnblockUser
{
    internal class UnblockUserCommandHandler : ICommandHandler<UnblockUserCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IFriendshipRepository _friendshipRepository;

        public UnblockUserCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            IFriendshipRepository friendshipRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _friendshipRepository = friendshipRepository;
        }

        public async Task<Unit> Handle(UnblockUserCommand request, CancellationToken cancellationToken)
        {
            var blockedUserId = new UserId(request.BlockedUserId);
            var unblockingUserId = new UserId(_executionContextAccessor.UserId);

            var members = new FriendshipMembers(blockedUserId, unblockingUserId);

            var friendship = await _friendshipRepository.GetByMembersAsync(members);
            
            friendship.Unblock(unblockingUserId, _friendshipRepository);
            
            return Unit.Value;
        }
    }
}