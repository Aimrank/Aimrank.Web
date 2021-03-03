using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Friendships;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Friendships.BlockUser
{
    internal class BlockUserCommandHandler : ICommandHandler<BlockUserCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IFriendshipRepository _friendshipRepository;

        public BlockUserCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            IFriendshipRepository friendshipRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _friendshipRepository = friendshipRepository;
        }

        public async Task<Unit> Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var blockingUserId = new UserId(_executionContextAccessor.UserId);
            var blockedUserId = new UserId(request.BlockedUserId);

            var members = new FriendshipMembers(blockingUserId, blockedUserId);

            var friendship = await _friendshipRepository.GetByMembersAsync(members);
            if (friendship is null)
            {
                var blockedFriendship = await Friendship.CreateAsync(members, null, blockingUserId);
                _friendshipRepository.Add(blockedFriendship);
            }
            else
            {
                friendship.Block(blockingUserId);
                _friendshipRepository.Update(friendship);
            }
            
            return Unit.Value;
        }
    }
}