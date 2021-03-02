using System.Threading.Tasks;

namespace Aimrank.Domain.Friendships
{
    public interface IFriendshipRepository
    {
        Task<Friendship> GetByMembersAsync(FriendshipMembers members);
        Task<bool> ExistsForMembersAsync(FriendshipMembers members);
        void Add(Friendship friendship);
        void Update(Friendship friendship);
        void Delete(Friendship friendship);
    }
}