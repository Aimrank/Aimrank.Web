using Aimrank.Application.Commands.RefreshJwt;
using Aimrank.Application.Contracts;

namespace Aimrank.Application.Commands.SignIn
{
    public class SignInCommand : ICommand<AuthenticationSuccessDto>
    {
        public string UsernameOrEmail { get; }
        public string Password { get; }

        public SignInCommand(string usernameOrEmail, string password)
        {
            UsernameOrEmail = usernameOrEmail;
            Password = password;
        }
    }
}