using System;

namespace Aimrank.Modules.UserAccess.Application.Services
{
    public interface IUrlFactory
    {
        string CreateEmailConfirmationLink(Guid userId, string token);
        string CreateResetPasswordLink(Guid userId, string token);
    }
}