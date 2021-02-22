using Aimrank.Common.Application.Exceptions;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.UnitTests.Mock
{
    internal class UserRepositoryMock : IUserRepository
    {
        private readonly Dictionary<UserId, User> _users = new();

        public IEnumerable<User> Users => _users.Values;

        public Task<IEnumerable<User>> BrowseByIdAsync(IEnumerable<UserId> ids)
            => Task.FromResult(_users.Values.Where(u => ids.Contains(u.Id)));

        public Task<User> GetByIdAsync(UserId id)
        {
            var user = _users.GetValueOrDefault(id);
            if (user is null)
            {
                throw new EntityNotFoundException();
            }

            return Task.FromResult(user);
        }

        public Task<bool> ExistsEmailAsync(string email)
            => Task.FromResult(_users.Values.Any(u => u.Email == email));

        public Task<bool> ExistsUsernameAsync(string username)
            => Task.FromResult(_users.Values.Any(u => u.Username == username));

        public Task<bool> ExistsSteamIdAsync(string steamId, UserId userId)
            => Task.FromResult(_users.Values.Any(u => u.SteamId == steamId));

        public Task<bool> AddAsync(User user, string password)
        {
            if (_users.ContainsKey(user.Id))
            {
                return Task.FromResult(false);
            }
            
            _users.Add(user.Id, user);

            return Task.FromResult(true);
        }

        public Task UpdateAsync(User user)
        {
            _users[user.Id] = user;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(User user)
        {
            _users.Remove(user.Id);
            return Task.CompletedTask;
        }
    }
}