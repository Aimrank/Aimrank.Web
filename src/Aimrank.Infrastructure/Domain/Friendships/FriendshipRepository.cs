using Aimrank.Common.Application.Exceptions;
using Aimrank.Domain.Friendships;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Domain.Friendships
{
    internal class FriendshipRepository : IFriendshipRepository
    {
        private readonly AimrankContext _context;

        public FriendshipRepository(AimrankContext context)
        {
            _context = context;
        }

        public async Task<Friendship> GetByMembersAsync(FriendshipMembers members)
        {
            var friendship = await _context.Friendships.FirstOrDefaultAsync(f =>
                (f.Members.UserId1 == members.UserId1 && f.Members.UserId2 == members.UserId2) ||
                (f.Members.UserId1 == members.UserId2 && f.Members.UserId2 == members.UserId1));

            if (friendship is null)
            {
                throw new EntityNotFoundException();
            }

            return friendship;
        }

        public Task<bool> ExistsForMembersAsync(FriendshipMembers members)
            => _context.Friendships.AnyAsync(f =>
                (f.Members.UserId1 == members.UserId1 && f.Members.UserId2 == members.UserId2) ||
                (f.Members.UserId1 == members.UserId2 && f.Members.UserId2 == members.UserId1));

        public void Add(Friendship friendship) => _context.Friendships.Add(friendship);

        public void Update(Friendship friendship) => _context.Friendships.Update(friendship);

        public void Delete(Friendship friendship) => _context.Friendships.Remove(friendship);
    }
}