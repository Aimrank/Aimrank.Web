using Aimrank.Web.Common.Application;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Domain.Friendships;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.BlockUser
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

            if (await _friendshipRepository.ExistsForMembersAsync(members))
            {
                var friendship = await _friendshipRepository.GetByMembersAsync(members);
                friendship.Block(blockingUserId);
            }
            else
            {
                var blockedFriendship = await Friendship.CreateAsync(members, _friendshipRepository, null, blockingUserId);
                _friendshipRepository.Add(blockedFriendship);
            }
            
            return Unit.Value;
        }
    }
}