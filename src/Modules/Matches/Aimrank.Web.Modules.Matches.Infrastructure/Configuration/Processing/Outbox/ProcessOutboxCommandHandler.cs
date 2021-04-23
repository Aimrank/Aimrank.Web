using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Outbox
{
    internal class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ProcessOutboxCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IMediator mediator,
            ILogger logger)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Unit> Handle(ProcessOutboxCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = @"
                SELECT
                    [OutboxMessage].[Id],
                    [OutboxMessage].[Type],
                    [OutboxMessage].[Data]
                FROM [matches].[OutboxMessages] AS [OutboxMessage]
                WHERE [OutboxMessage].[ProcessedDate] IS NULL
                ORDER BY [OutboxMessage].[OccurredAt];";

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
            
            const string sqlUpdate = @"
                UPDATE [matches].[OutboxMessages]
                SET [ProcessedDate] = @Date
                WHERE [Id] = @Id;";

            foreach (var message in messages)
            {
                var notification = DeserializeMessage(message);
                
                try
                {
                    await _mediator.Publish(notification, cancellationToken);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, exception.Message);
                }

                await connection.ExecuteScalarAsync(sqlUpdate, new
                {
                    Date = DateTime.UtcNow,
                    message.Id
                });
            }

            return Unit.Value;
        }

        private static IDomainEventNotification DeserializeMessage(OutboxMessageDto message)
        {
            var messageAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(assembly => message.Type.Contains(assembly.GetName().Name));

            var messageType = messageAssembly.GetType(message.Type);

            return JsonSerializer.Deserialize(message.Data, messageType) as IDomainEventNotification;
        }
    }
}