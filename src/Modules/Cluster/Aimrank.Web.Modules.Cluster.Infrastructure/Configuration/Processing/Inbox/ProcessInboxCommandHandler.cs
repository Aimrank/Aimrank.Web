using Aimrank.Web.Modules.Cluster.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Processing.Inbox
{
    internal class ProcessInboxCommandHandler : ICommandHandler<ProcessInboxCommand>
    {
        private readonly ClusterContext _context;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ProcessInboxCommandHandler(
            ClusterContext context,
            IMediator mediator,
            ILogger logger)
        {
            _context = context;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Unit> Handle(ProcessInboxCommand request, CancellationToken cancellationToken)
        {
            var messages = await _context.InboxMessages
                .Where(m => m.ProcessedDate == null)
                .OrderBy(m => m.OccurredAt)
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

        private static INotification DeserializeMessage(InboxMessage message)
        {
            var messageAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(assembly => message.Type.Contains(assembly.GetName().Name));

            var messageType = messageAssembly.GetType(message.Type);
            
            return JsonSerializer.Deserialize(message.Data, messageType) as INotification;
        }
    }
}