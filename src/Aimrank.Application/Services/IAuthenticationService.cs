using Aimrank.Domain.Users;
using System.Threading.Tasks;

namespace Aimrank.Application.Services
{
    public interface IAuthenticationService
    {
        Task<User> AuthenticateUserWithEmailAsync(string email, string password);
        Task<User> AuthenticateUserWithUsernameAsync(string username, string password);
    }
}