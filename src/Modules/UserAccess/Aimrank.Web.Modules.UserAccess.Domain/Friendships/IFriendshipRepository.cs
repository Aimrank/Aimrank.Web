using System.Threading.Tasks;

namespace Aimrank.Web.Modules.UserAccess.Domain.Friendships
{
    public interface IFriendshipRepository
    {
        Task<Friendship> GetByMembersAsync(FriendshipMembers members);
        Task<bool> ExistsForMembersAsync(FriendshipMembers members);
        void Add(Friendship friendship);
        void Delete(Friendship friendship);
    }
}