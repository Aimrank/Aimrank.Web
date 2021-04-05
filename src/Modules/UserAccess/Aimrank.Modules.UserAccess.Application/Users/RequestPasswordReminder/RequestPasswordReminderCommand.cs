using Aimrank.Modules.UserAccess.Application.Contracts;

namespace Aimrank.Modules.UserAccess.Application.Users.RequestPasswordReminder
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