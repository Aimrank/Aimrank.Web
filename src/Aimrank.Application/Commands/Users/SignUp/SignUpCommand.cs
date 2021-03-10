using Aimrank.Application.Commands.Users.RefreshJwt;
using Aimrank.Application.Contracts;

namespace Aimrank.Application.Commands.Users.SignUp
{
    public class SignUpCommand : ICommand<AuthenticationSuccessDto>
    {
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        public string PasswordRepeat { get; }

        public SignUpCommand(string username, string email, string password, string passwordRepeat)
        {
            Username = username;
            Email = email;
            Password = password;
            PasswordRepeat = passwordRepeat;
        }
    }
}