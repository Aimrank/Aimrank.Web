using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Dapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Domain.Users.RemoveExpiredTokens
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
                FROM [users].[UsersTokens]
                WHERE DATEDIFF(SECOND, GETUTCDATE(), [ExpiresAt]) <= 0;";

            await connection.ExecuteAsync(sql);
            
            return Unit.Value;
        }
    }
}