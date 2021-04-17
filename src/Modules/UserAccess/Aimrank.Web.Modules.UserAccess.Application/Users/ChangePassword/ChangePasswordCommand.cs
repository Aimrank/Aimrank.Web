using Aimrank.Web.Modules.UserAccess.Application.Contracts;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.ChangePassword
{
    public class ChangePasswordCommand : ICommand
    {
        public string OldPassword { get; }
        public string NewPassword { get; }
        public string RepeatNewPassword { get; }

        public ChangePasswordCommand(string oldPassword, string newPassword, string repeatNewPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
            RepeatNewPassword = repeatNewPassword;
        }
    }
}