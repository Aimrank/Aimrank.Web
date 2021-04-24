using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Dapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing.RemoveProcessedMessages
{
    internal class RemoveProcessedMessagesCommandHandler : ICommandHandler<RemoveProcessedMessagesCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public RemoveProcessedMessagesCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(RemoveProcessedMessagesCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = @"
                DELETE FROM users.outbox_messages
                WHERE EXTRACT(EPOCH FROM NOW() - processed_date) / 3600 >= 2;";

            await connection.ExecuteAsync(sql);
            
            return Unit.Value;
        }
    }
}