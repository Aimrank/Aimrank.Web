using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Data;
using Aimrank.Common.Application.Events;
using Aimrank.Common.Infrastructure.EventBus;
using Dapper;
using MediatR;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Infrastructure.Configuration.Outbox
{
    internal class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IEventBus _eventBus;

        public ProcessOutboxCommandHandler(ISqlConnectionFactory sqlConnectionFactory, IEventBus eventBus)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(ProcessOutboxCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql =
                @"SELECT
                    [OutboxMessage].[Id],
                    [OutboxMessage].[Type],
                    [OutboxMessage].[Data]
                  FROM [aimrank].[OutboxMessages] AS [OutboxMessage]
                  ORDER BY [OutboxMessage].[OccurredAt];";

            var messages = await connection.QueryAsync<OutboxMessageDto>(sql);

            const string sqlUpdate = "DELETE FROM [aimrank].[OutboxMessages] WHERE [Id] = @Id;";

            foreach (var message in messages)
            {
                var type = Assemblies.IntegrationEvents.GetType(message.Type);
                var @event = (IIntegrationEvent) JsonSerializer.Deserialize(message.Data, type);

                await _eventBus.Publish(@event);

                await connection.ExecuteAsync(sqlUpdate, new {message.Id});
            }
            
            return Unit.Value;
        }
    }
}