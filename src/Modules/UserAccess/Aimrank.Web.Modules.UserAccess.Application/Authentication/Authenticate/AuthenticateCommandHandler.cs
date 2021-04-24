using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using Dapper;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System;

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
                    id,
                    email,
                    username,
                    password,
                    is_active
                FROM users.users
                WHERE email = @UsernameOrEmail OR username = @UsernameOrEmail;";

            var user = await connection.QueryFirstOrDefaultAsync<UserResult>(sql, new {request.UsernameOrEmail});

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

        private class UserResult
        {
            public Guid Id { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public bool IsActive { get; set; }
        }
    }
}