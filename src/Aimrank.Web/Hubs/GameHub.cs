using Aimrank.Web.Events;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Hubs
{
    public class GameHub : Hub<IGameClient>
    {
        private readonly EventBus _eventBus;

        public GameHub(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task PublishEvent(Guid serverId, string content)
        {
            await _eventBus.PublishAsync(serverId, content);
        }
    }
}