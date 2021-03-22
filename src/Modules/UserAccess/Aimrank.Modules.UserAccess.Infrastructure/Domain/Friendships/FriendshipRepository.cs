using Aimrank.Common.Application.Exceptions;
using Aimrank.Modules.UserAccess.Domain.Friendships;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aimrank.Modules.UserAccess.Infrastructure.Domain.Friendships
{
    internal class FriendshipRepository : IFriendshipRepository
    {
        private readonly UserAccessContext _context;

        public FriendshipRepository(UserAccessContext context)
        {
            _context = context;
        }

        public async Task<Friendship> GetByMembersAsync(FriendshipMembers members)
        {
            var friendship = await _context.Friendships.FirstOrDefaultAsync(f =>
                (f.User1Id == members.UserId1 && f.User2Id == members.UserId2) ||
                (f.User1Id == members.UserId2 && f.User2Id == members.UserId1));

            if (friendship is null)
            {
                throw new EntityNotFoundException();
            }

            return friendship;
        }

        public Task<bool> ExistsForMembersAsync(FriendshipMembers members)
            => _context.Friendships.AnyAsync(f =>
                (f.User1Id == members.UserId1 && f.User2Id == members.UserId2) ||
                (f.User1Id == members.UserId2 && f.User2Id == members.UserId1));

        public void Add(Friendship friendship) => _context.Friendships.Add(friendship);

        public void Delete(Friendship friendship) => _context.Friendships.Remove(friendship);
    }
}