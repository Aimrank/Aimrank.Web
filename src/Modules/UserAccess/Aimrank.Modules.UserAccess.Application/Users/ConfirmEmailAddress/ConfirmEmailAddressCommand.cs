using Aimrank.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Modules.UserAccess.Application.Users.ConfirmEmailAddress
{
    public class ConfirmEmailAddressCommand : ICommand
    {
        public Guid UserId { get; }
        public string Token { get; }

        public ConfirmEmailAddressCommand(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}