using Aimrank.Modules.UserAccess.Application.Contracts;

namespace Aimrank.Modules.UserAccess.Application.Users.RequestEmailConfirmation
{
    public class RequestEmailConfirmationCommand : ICommand
    {
        public string UsernameOrEmail { get; }

        public RequestEmailConfirmationCommand(string usernameOrEmail)
        {
            UsernameOrEmail = usernameOrEmail;
        }
    }
}