using Aimrank.Application.Services;
using Aimrank.Domain.Users;
using Aimrank.Infrastructure.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Application.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<UserModel> _userManager;

        public AuthenticationService(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> AuthenticateUserWithEmailAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, password);
            return result ? user.AsUser() : null;
        }

        public async Task<User> AuthenticateUserWithUsernameAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, password);
            return result ? user.AsUser() : null;
        }
    }
}