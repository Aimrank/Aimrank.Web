using Aimrank.Application.Queries.GetServerProcesses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Application.CSGO
{
    public interface IServerProcessManager
    {
        IEnumerable<ServerProcessDto> GetProcesses();
        string StartServer(Guid serverId, IEnumerable<string> whitelist, string map);
        Task StopServerAsync(Guid serverId);
        Task ExecuteCommandAsync(Guid serverId, string command);
    }
}