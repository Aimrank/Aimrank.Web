using System.Threading.Tasks;
using System;

namespace Aimrank.Application.CSGO
{
    public interface IServerEventNotifier
    {
        Task NotifyAsync(Guid serverId, string content);
    }
}