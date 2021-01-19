using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.Server
{
    public class ServerProcessManager : IDisposable
    {
        private readonly Dictionary<Guid, ServerProcess> _processes = new();
        
        private readonly IHubContext<GameHub, IGameClient> _hubContext;

        public ServerProcessManager(IHubContext<GameHub, IGameClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public IEnumerable<ServerProcess> GetProcesses() => _processes.Values;

        public bool StartServer(Guid serverId)
        {
            if (_processes.ContainsKey(serverId))
            {
                return false;
            }
            
            var process = new ServerProcess(serverId);
            
            process.MessageReceived += (_, ea) =>
            {
                _hubContext.Clients.All.ServerMessageReceived(ea.Content);
            };

            process.StatusChanged += (_, ea) =>
            {
                if (ea.Status == ServerProcessStatus.Exited)
                {
                    _processes[ea.Id].Dispose();
                    _processes.Remove(ea.Id);
                }
            };
            
            _processes.Add(serverId, process);
            
            process.Start();

            return true;
        }

        public bool StopServer(Guid serverId)
        {
            if (!_processes.ContainsKey(serverId))
            {
                return false;
            }
            
            _processes[serverId].Stop();

            return true;
        }

        public void ExecuteCommand(Guid serverId, string command)
        {
            if (!_processes.ContainsKey(serverId))
            {
                return;
            }

            _processes[serverId].Execute(command);
        }

        public void Dispose()
        {
            foreach (var process in _processes.Values)
            {
                process.Stop();
            }
        }
    }
}