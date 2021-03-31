using System.Threading.Tasks;

namespace Aimrank.Modules.UserAccess.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(UserId id);
        Task<User> GetByEmailOptionalAsync(string email);
        Task<User> GetByUsernameOptionalAsync(string username);
        Task<bool> ExistsEmailAsync(string email);
        Task<bool> ExistsUsernameAsync(string username);
        void Add(User user);
    }
}