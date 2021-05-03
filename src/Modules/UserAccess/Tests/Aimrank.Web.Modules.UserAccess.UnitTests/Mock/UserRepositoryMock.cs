using Aimrank.Web.Common.Application.Exceptions;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.UserAccess.UnitTests.Mock
{
    internal class UserRepositoryMock : IUserRepository
    {
        private readonly Dictionary<UserId, User> _users = new();

        public Task<User> GetByIdAsync(UserId id)
        {
            var user = _users.GetValueOrDefault(id);
            if (user is null)
            {
                throw new EntityNotFoundException();
            }

            return Task.FromResult(user);
        }

        public Task<User> GetByEmailOptionalAsync(string email)
            => Task.FromResult(_users.Values.FirstOrDefault(u => u.Email == email.ToLower()));

        public Task<User> GetByUsernameOptionalAsync(string username)
            => Task.FromResult(_users.Values.FirstOrDefault(u => u.Username == username));

        public Task<bool> ExistsEmailAsync(string email)
            => Task.FromResult(_users.Values.Any(u => u.Email == email.ToLower()));

        public Task<bool> ExistsUsernameAsync(string username)
            => Task.FromResult(_users.Values.Any(u => u.Username == username));

        public void Add(User user) => _users.Add(user.Id, user);
    }
}