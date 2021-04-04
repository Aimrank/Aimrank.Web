using Aimrank.Common.Application.Data;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Dapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Modules.UserAccess.Infrastructure.Domain.Users.RemoveExpiredTokens
{
    internal class RemoveExpiredTokensCommandHandler : ICommandHandler<RemoveExpiredTokensCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public RemoveExpiredTokensCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(RemoveExpiredTokensCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                DELETE
                FROM [users].[UsersTokens] AS [T]
                WHERE [T].[ExpiresAt] >= @UtcNow;";

            await connection.ExecuteAsync(sql, new {DateTime.UtcNow});
            
            return Unit.Value;
        }
    }
}