using Aimrank.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Domain.Users
{
    internal class UserRepository : IUserRepository
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly AimrankContext _context;
        
        public UserRepository(UserManager<UserModel> userManager, AimrankContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<User> GetByIdAsync(UserId id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            return user.AsUser();
        }

        public Task<bool> ExistsEmailAsync(string email) => _context.Users.AnyAsync(u => u.Email == email);

        public Task<bool> ExistsUsernameAsync(string username) => _context.Users.AnyAsync(u => u.UserName == username);

        public async Task<bool> AddAsync(User user, string password)
        {
            var model = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                SteamId = user.SteamId,
                UserName = user.Username
            };
            
            var result = await _userManager.CreateAsync(model, password);
            return result.Succeeded;
        }

        public async Task UpdateAsync(User user)
        {
            var model = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (model is null)
            {
                return;
            }

            model.Email = user.Email;
            model.SteamId = user.SteamId;
            model.UserName = user.Username;
        }

        public async Task DeleteAsync(User user)
        {
            var model = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (model is null)
            {
                return;
            }
            
            _context.Users.Remove(model);
        }
    }
}