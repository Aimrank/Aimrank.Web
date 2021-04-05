using Aimrank.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Modules.UserAccess.Application.Users.ResetPassword
{
    public class ResetPasswordCommand : ICommand
    {
        public Guid UserId { get; }
        public string Token { get; }
        public string NewPassword { get; }
        public string RepeatNewPassword { get; }

        public ResetPasswordCommand(Guid userId, string token, string newPassword, string repeatNewPassword)
        {
            UserId = userId;
            Token = token;
            NewPassword = newPassword;
            RepeatNewPassword = repeatNewPassword;
        }
    }
}