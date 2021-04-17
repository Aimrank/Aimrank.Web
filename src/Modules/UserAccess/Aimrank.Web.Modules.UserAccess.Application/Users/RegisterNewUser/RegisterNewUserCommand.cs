using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.RegisterNewUser
{
    public class RegisterNewUserCommand : ICommand<Guid>
    {
        public string Email { get; }
        public string Username { get; }
        public string Password { get; }
        public string PasswordRepeat { get; }

        public RegisterNewUserCommand(string email, string username, string password, string passwordRepeat)
        {
            Email = email;
            Username = username;
            Password = password;
            PasswordRepeat = passwordRepeat;
        }
    }
}