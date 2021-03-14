using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Modules.UserAccess.Domain.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> BrowseByIdAsync(IEnumerable<UserId> ids);
        Task<User> GetByIdAsync(UserId id);
        Task<bool> ExistsEmailAsync(string email);
        Task<bool> ExistsUsernameAsync(string username);
        Task<bool> ExistsSteamIdAsync(string steamId, UserId userId);
        void Add(User user);
        void Update(User user);
    }
}