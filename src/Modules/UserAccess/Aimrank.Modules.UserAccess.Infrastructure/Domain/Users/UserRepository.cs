using Aimrank.Common.Application.Data;
using Aimrank.Common.Application.Exceptions;
using Aimrank.Modules.UserAccess.Domain.Users;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aimrank.Modules.UserAccess.Infrastructure.Domain.Users
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

        public async Task<bool> ExistsEmailAsync(string email)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT COUNT (*) FROM [users].[Users] AS [U] WHERE [U].[Email] = @Email;";
            var count = await connection.ExecuteScalarAsync<int>(sql, new {Email = email});
            return count > 0;
        }

        public async Task<bool> ExistsUsernameAsync(string username)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT COUNT (*) FROM [users].[Users] AS [U] WHERE [U].[Username] = @Username;";
            var count = await connection.ExecuteScalarAsync<int>(sql, new {Username = username});
            return count > 0;
        }

        public void Add(User user) => _context.Users.Add(user);
    }
}