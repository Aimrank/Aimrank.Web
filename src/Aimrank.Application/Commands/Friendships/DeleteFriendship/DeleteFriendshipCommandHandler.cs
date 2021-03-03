using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Friendships;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Friendships.DeleteFriendship
{
    internal class DeleteFriendshipCommandHandler : ICommandHandler<DeleteFriendshipCommand>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IFriendshipRepository _friendshipRepository;

        public DeleteFriendshipCommandHandler(
            IExecutionContextAccessor executionContextAccessor,
            IFriendshipRepository friendshipRepository)
        {
            _executionContextAccessor = executionContextAccessor;
            _friendshipRepository = friendshipRepository;
        }

        public async Task<Unit> Handle(DeleteFriendshipCommand request, CancellationToken cancellationToken)
        {
            var deletingUserId = new UserId(_executionContextAccessor.UserId);
            var deletedUserId = new UserId(request.UserId);

            var members = new FriendshipMembers(deletingUserId, deletedUserId);

            var friendship = await _friendshipRepository.GetByMembersAsync(members);
            
            friendship.Delete(deletingUserId, _friendshipRepository);
            
            return Unit.Value;
        }
    }
}