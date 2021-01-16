using Aimrank.EventBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Services
{
    public class EventBusHostedService : BackgroundService
    {
        private readonly ILogger<EventBusHostedService> _logger;

        public EventBusHostedService(ILogger<EventBusHostedService> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var receiver = new BusReceiver();

            receiver.MessageReceived += (_, ea) =>
            {
                _logger.LogInformation($"Received: [{ea.Content}]");
            };
            
            return Task.Run(() => receiver.Listen(), stoppingToken);
        }
    }
}