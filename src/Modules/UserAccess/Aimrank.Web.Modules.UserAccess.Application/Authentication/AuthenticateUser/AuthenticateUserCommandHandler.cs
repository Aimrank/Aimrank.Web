using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.UserAccess.Application.Authentication.Authenticate;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Authentication.AuthenticateUser
{
    internal class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, AuthenticationResult>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AuthenticateUserCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<AuthenticationResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT id, email, username
                FROM users.users
                WHERE id = @UserId;";

            var user = await connection.QueryFirstAsync<UserResult>(sql, new {request.UserId});
            if (user is null)
            {
                throw new InvalidCredentialsException();
            }
            
            return new AuthenticationResult(new AuthenticatedUserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Claims = new List<Claim>
                {
                    new(CustomClaimTypes.Id, user.Id.ToString()),
                    new(CustomClaimTypes.Name, user.Username),
                    new(CustomClaimTypes.Email, user.Email)
                }
            });
        }

        private record UserResult(Guid Id, string Email, string Username);
    }
}