using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Authentication.Authenticate
{
    internal class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, AuthenticationResult>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AuthenticateCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<AuthenticationResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
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
                WHERE email = LOWER(@UsernameOrEmail) OR username = @UsernameOrEmail;";

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
                new {request.UsernameOrEmail},
                splitOn: "name");

            if (user is null)
            {
                throw new InvalidCredentialsException();
            }

            if (!PasswordManager.VerifyPassword(user.Password, request.Password))
            {
                throw new InvalidCredentialsException();
            }

            if (!user.IsActive)
            {
                throw new EmailNotConfirmedException();
            }

            return new AuthenticationResult(user.AsAuthenticatedUserDto());
        }
    }
}