using System.Threading.Tasks;

namespace Aimrank.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(UserId id);
        Task<bool> ExistsEmailAsync(string email);
        Task<bool> ExistsUsernameAsync(string username);
        Task<bool> AddAsync(User user, string password);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}