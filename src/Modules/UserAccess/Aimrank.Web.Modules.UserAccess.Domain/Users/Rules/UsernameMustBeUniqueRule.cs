using Aimrank.Web.Common.Domain;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users.Rules
{
    public class UsernameMustBeUniqueRule : IAsyncBusinessRule
    {
        private readonly IUserRepository _userRepository;
        private readonly string _username;

        public UsernameMustBeUniqueRule(IUserRepository userRepository, string username)
        {
            _userRepository = userRepository;
            _username = username;
        }

        public string Message => "This username is already taken";
        public string Code => "username_not_unique";

        public Task<bool> IsBrokenAsync() => _userRepository.ExistsUsernameAsync(_username);
    }
}