using Aimrank.Common.Domain;
using Aimrank.Modules.UserAccess.Domain.Users.Rules;
using System.Threading.Tasks;

namespace Aimrank.Modules.UserAccess.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; }
        private string _password;
        private string _username;
        private string _email;
        private string _steamId;

        private User() {}

        private User(UserId id, string email, string username, string password)
        {
            Id = id;
            _email = email;
            _username = username;
            _password = password;
        }

        public static async Task<User> CreateAsync(
            UserId id,
            string email,
            string username,
            string password,
            IUserRepository userRepository)
        {
            await BusinessRules.CheckAsync(new EmailMustBeUniqueRule(userRepository, email));
            await BusinessRules.CheckAsync(new UsernameMustBeUniqueRule(userRepository, username));

            return new User(id, email, username, password);
        }
        
        public async Task SetSteamIdAsync(string steamId, IUserRepository userRepository)
        {
            BusinessRules.Check(new SteamIdMustBeValidRule(steamId));
            await BusinessRules.CheckAsync(new SteamIdMustBeUniqueRule(userRepository, steamId, Id));

            _steamId = steamId;
        }
    }
}