using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Application.Exceptions;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Domain.Users
{
    internal class UserRepository : IUserRepository
    {
        private readonly UserAccessContext _context;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public UserRepository(UserAccessContext context, ISqlConnectionFactory sqlConnectionFactory)
        {
            _context = context;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<User> GetByIdAsync(UserId id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                throw new EntityNotFoundException();
            }

            return user;
        }

        public Task<User> GetByEmailOptionalAsync(string email) =>
            _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());

        public Task<User> GetByUsernameOptionalAsync(string username) =>
            _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        public async Task<bool> ExistsEmailAsync(string email)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT COUNT (*) FROM users.users WHERE email = @Email;";
            var count = await connection.ExecuteScalarAsync<int>(sql, new {Email = email.ToLower()});
            return count > 0;
        }

        public async Task<bool> ExistsUsernameAsync(string username)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT COUNT (*) FROM users.users WHERE username = @Username;";
            var count = await connection.ExecuteScalarAsync<int>(sql, new {Username = username});
            return count > 0;
        }

        public void Add(User user) => _context.Users.Add(user);
    }
}