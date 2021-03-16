using Aimrank.Common.Application.Data;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Dapper;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Modules.UserAccess.Application.Authentication.Authenticate
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
                    [U].[Id],
                    [U].[Email],
                    [U].[Username],
                    [U].[Password]
                FROM [users].[Users] AS [U]
                WHERE
                    [U].[Email] = @UsernameOrEmail OR
                    [U].[Username] = @UsernameOrEmail;";

            var user = await connection.QueryFirstOrDefaultAsync<UserResult>(sql, new {request.UsernameOrEmail});

            if (user is null)
            {
                return AuthenticationResult.Error("Invalid credentials");
            }

            if (!PasswordManager.VerifyPassword(user.Password, request.Password))
            {
                return AuthenticationResult.Error("Invalid credentials");
            }
            
            return AuthenticationResult.Success(new AuthenticatedUserDto
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
        }
    }
}