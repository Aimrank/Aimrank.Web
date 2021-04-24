using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Dapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Processing.RemoveProcessedMessages
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
                DELETE FROM [cluster].[InboxMessages]
                WHERE DATEDIFF(HOUR, [ProcessedDate], GETUTCDATE()) >= 2;";

            await connection.ExecuteAsync(sql);
            
            return Unit.Value;
        }
    }
}