using Aimrank.Modules.UserAccess.Application.Authentication.Authenticate;
using Aimrank.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Modules.UserAccess.Application.Authentication.AuthenticateUser
{
    public class AuthenticateUserCommand : ICommand<AuthenticationResult>
    {
        public Guid UserId { get; }

        public AuthenticateUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}