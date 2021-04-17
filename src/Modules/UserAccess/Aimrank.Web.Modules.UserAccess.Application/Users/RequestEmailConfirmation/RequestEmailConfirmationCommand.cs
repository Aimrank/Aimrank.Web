using Aimrank.Web.Modules.UserAccess.Application.Contracts;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.RequestEmailConfirmation
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