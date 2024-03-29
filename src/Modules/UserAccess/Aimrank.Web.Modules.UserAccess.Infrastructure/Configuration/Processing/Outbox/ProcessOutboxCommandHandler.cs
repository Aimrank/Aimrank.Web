using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox
{
    internal class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand>
    {
        private readonly UserAccessContext _context;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ProcessOutboxCommandHandler(
            UserAccessContext context,
            IMediator mediator,
            ILogger logger)
        {
            _context = context;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Unit> Handle(ProcessOutboxCommand request, CancellationToken cancellationToken)
        {
            var messages = await _context.OutboxMessages
                .Where(m => m.ProcessedDate == null)
                .OrderBy(m => m.OccurredOn)
                .ToListAsync(cancellationToken);

            foreach (var message in messages)
            {
                try
                {
                    var notification = DeserializeMessage(message);
                    
                    await _mediator.Publish(notification, cancellationToken);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, exception.Message);
                }
                
                message.ProcessedDate = DateTime.UtcNow;
            }

            return Unit.Value;
        }

        private static IDomainEventNotification DeserializeMessage(OutboxMessage message)
        {
            var messageAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(assembly => message.Type.Contains(assembly.GetName().Name));

            var messageType = messageAssembly.GetType(message.Type);
            
            return JsonSerializer.Deserialize(message.Data, messageType) as IDomainEventNotification;
        }
    }
}