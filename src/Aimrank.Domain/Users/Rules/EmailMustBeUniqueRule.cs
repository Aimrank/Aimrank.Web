using Aimrank.Common.Domain;
using System.Threading.Tasks;

namespace Aimrank.Domain.Users.Rules
{
    public class EmailMustBeUniqueRule : IAsyncBusinessRule
    {
        private readonly IUserRepository _userRepository;
        private readonly string _email;

        public EmailMustBeUniqueRule(IUserRepository userRepository, string email)
        {
            _userRepository = userRepository;
            _email = email;
        }

        public string Message => "This E-Mail is already taken";
        public string Code => "email_not_unique";

        public Task<bool> IsBrokenAsync() => _userRepository.ExistsEmailAsync(_email);
    }
}