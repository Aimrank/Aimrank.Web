using Aimrank.Common.Domain;
using Aimrank.Domain.Users.Rules;
using System.Threading.Tasks;

namespace Aimrank.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string SteamId { get; private set; }

        private User() {}

        private User(UserId id, string email, string username)
        {
            Id = id;
            Email = email;
            Username = username;
        }

        public static async Task<User> CreateAsync(
            UserId id,
            string email,
            string username,
            IUserRepository userRepository)
        {
            await BusinessRules.CheckAsync(new EmailMustBeUniqueRule(userRepository, email));
            await BusinessRules.CheckAsync(new UsernameMustBeUniqueRule(userRepository, username));

            return new User(id, email, username);
        }
        
        public async Task SetSteamIdAsync(string steamId, IUserRepository userRepository)
        {
            BusinessRules.Check(new SteamIdMustBeValidRule(steamId));
            await BusinessRules.CheckAsync(new SteamIdMustBeUniqueRule(userRepository, steamId, Id));

            SteamId = steamId;
        }
    }
}