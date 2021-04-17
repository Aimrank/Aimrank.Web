using Aimrank.Web.Modules.UserAccess.Application.Contracts;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.RequestPasswordReminder
{
    public class RequestPasswordReminderCommand : ICommand
    {
        public string UsernameOrEmail { get; }

        public RequestPasswordReminderCommand(string usernameOrEmail)
        {
            UsernameOrEmail = usernameOrEmail;
        }
    }
}