using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Domain.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> BrowseByIdAsync(IEnumerable<UserId> ids);
        Task<User> GetByIdAsync(UserId id);
        Task<bool> ExistsEmailAsync(string email);
        Task<bool> ExistsUsernameAsync(string username);
        Task<bool> ExistsSteamIdAsync(string steamId, UserId userId);
        Task<bool> AddAsync(User user, string password);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}