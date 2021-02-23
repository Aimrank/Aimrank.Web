using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Users.RefreshJwt
{
    public class RefreshJwtCommand : ICommand<AuthenticationSuccessDto>
    {
        public Guid RefreshToken { get; }
        public string Jwt { get; }

        public RefreshJwtCommand(Guid refreshToken, string jwt)
        {
            RefreshToken = refreshToken;
            Jwt = jwt;
        }
    }
}