using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Application.CSGO
{
    public interface IServerProcessManager
    {
        string StartServer(Guid serverId, string map, IEnumerable<string> whitelist);
        void StopServer(Guid serverId);
        Task ExecuteCommandAsync(Guid serverId, string command);
    }
}