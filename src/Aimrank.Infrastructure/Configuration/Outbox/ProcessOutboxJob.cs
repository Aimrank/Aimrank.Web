using Aimrank.Common.Application.Data;
using Aimrank.Common.Application.Events;
using Aimrank.Common.Infrastructure.EventBus;
using Dapper;
using Quartz;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aimrank.Infrastructure.Configuration.Outbox
{
    [DisallowConcurrentExecution]
    internal class ProcessOutboxJob : IJob
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IEventBus _eventBus;

        public ProcessOutboxJob(ISqlConnectionFactory sqlConnectionFactory, IEventBus eventBus)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _eventBus = eventBus;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();

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
                var @event = (IntegrationEvent) JsonSerializer.Deserialize(message.Data, type);

                await _eventBus.Publish(@event);

                await connection.ExecuteAsync(sqlUpdate, new {message.Id});
            }
        }
    }
}