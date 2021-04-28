using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.UserAccess.Application.Authentication.Authenticate;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

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
                SELECT
                    u.id,
                    u.email,
                    u.username,
                    u.password,
                    u.is_active,
                    r.name
                FROM users.users AS u
                LEFT JOIN users.users_roles AS r ON r.user_id = u.id
                WHERE id = @UserId;";

            UserResultDto user = null;

            await connection.QueryAsync<UserResultDto, string, UserResultDto>(sql,
                (userResult, role) =>
                {
                    user ??= userResult;
                    user.Roles ??= new List<string>();
                    if (!string.IsNullOrEmpty(role))
                    {
                        user.Roles.Add(role);
                    }
                    return userResult;
                },
                new {request.UserId},
                splitOn: "name");

            if (user is null)
            {
                throw new InvalidCredentialsException();
            }

            return new AuthenticationResult(user.AsAuthenticatedUserDto());
        }
    }
}