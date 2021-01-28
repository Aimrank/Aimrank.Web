using Aimrank.Application.CSGO;
using Aimrank.Application.Events;
using Aimrank.Common.Infrastructure.EventBus;
using System.Threading.Tasks;
using System;

namespace Aimrank.Infrastructure.Application.CSGO
{
    internal class ServerEventNotifier : IServerEventNotifier
    {
        private readonly IEventBus _eventBus;

        public ServerEventNotifier(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task NotifyAsync(Guid serverId, string content)
            => _eventBus.Publish(new ServerMessageReceivedEvent(Guid.NewGuid(), serverId, content, DateTime.UtcNow));
    }
}