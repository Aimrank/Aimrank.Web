using Aimrank.Common.Domain;
using Aimrank.Modules.UserAccess.Domain.Users.Rules;
using System.Threading.Tasks;

namespace Aimrank.Modules.UserAccess.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; }
        public string Email { get; }
        public string Username { get; }
        private string _password;

        private User() {}

        private User(UserId id, string email, string username, string password)
        {
            Id = id;
            Email = email;
            Username = username;
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
    }
}