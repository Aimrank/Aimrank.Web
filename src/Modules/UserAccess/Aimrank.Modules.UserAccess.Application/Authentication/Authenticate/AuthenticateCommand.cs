using Aimrank.Modules.UserAccess.Application.Contracts;

namespace Aimrank.Modules.UserAccess.Application.Authentication.Authenticate
{
    public class AuthenticateCommand : ICommand<AuthenticationResult>
    {
        public string UsernameOrEmail { get; }
        public string Password { get; }

        public AuthenticateCommand(string usernameOrEmail, string password)
        {
            UsernameOrEmail = usernameOrEmail;
            Password = password;
        }
    }
}