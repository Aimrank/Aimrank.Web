using Aimrank.Web.Common.Application.Data;
using Aimrank.Web.Modules.Matches.Application.Contracts;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Inbox
{
    internal class ProcessInboxCommandHandler : ICommandHandler<ProcessInboxCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ProcessInboxCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IMediator mediator,
            ILogger logger)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Unit> Handle(ProcessInboxCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = @"
                SELECT
                    [InboxMessage].[Id],
                    [InboxMessage].[Type],
                    [InboxMessage].[Data]
                FROM [matches].[InboxMessages] AS [InboxMessage]
                WHERE [InboxMessage].[ProcessedDate] IS NULL
                ORDER BY [InboxMessage].[OccurredAt];";

            var messages = await connection.QueryAsync<InboxMessageDto>(sql);

            const string sqlUpdate = @"
                UPDATE [matches].[InboxMessages]
                SET [ProcessedDate] = @Date
                WHERE [Id] = @Id;";

            foreach (var message in messages)
            {
                var messageAssembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(assembly => message.Type.Contains(assembly.GetName().Name));

                var messageType = messageAssembly.GetType(message.Type);
                var notification = JsonSerializer.Deserialize(message.Data, messageType) as INotification;

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
    }
}