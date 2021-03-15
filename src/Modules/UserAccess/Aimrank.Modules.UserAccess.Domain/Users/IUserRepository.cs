using System.Threading.Tasks;

namespace Aimrank.Modules.UserAccess.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(UserId id);
        Task<bool> ExistsEmailAsync(string email);
        Task<bool> ExistsUsernameAsync(string username);
        void Add(User user);
    }
}