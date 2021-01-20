using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Server
{
    public class ServerProcessManager : IDisposable
    {
        private readonly object _locker = new();
        
        private readonly IHubContext<GameHub, IGameClient> _hubContext;
        
        private readonly ConcurrentQueue<int> _availablePorts = new();
        
        private readonly ConcurrentDictionary<Guid, ServerProcess> _processes = new();

        public ServerProcessManager(IHubContext<GameHub, IGameClient> hubContext)
        {
            _hubContext = hubContext;
            _availablePorts.Enqueue(27016);
            _availablePorts.Enqueue(27017);
            _availablePorts.Enqueue(27018);
            _availablePorts.Enqueue(27019);
        }

        public IEnumerable<ServerProcess> GetProcesses() => _processes.Values;

        public bool StartServer(Guid serverId)
        {
            lock (_locker)
            {
                if (_processes.ContainsKey(serverId))
                {
                    return false;
                }

                if (_availablePorts.TryDequeue(out var port))
                {
                    var process = new ServerProcess(serverId, port);
                    
                    process.EventReceived += (_, ea) => _hubContext.Clients.All.EventReceived(ea.Content);

                    if (_processes.TryAdd(serverId, process))
                    {
                        process.Start();
                        return true;
                    }
                }

                return false;
            }
        }

        public bool StopServer(Guid serverId)
        {
            lock (_locker)
            {
                if (_processes.TryRemove(serverId, out var process))
                {
                    _availablePorts.Enqueue(process.Port);
                    process.Dispose();
                    return true;
                }

                return false;
            }
        }

        public async Task ExecuteCommandAsync(Guid serverId, string command)
        {
            if (_processes.TryGetValue(serverId, out var process))
            {
                await process.ExecuteAsync(command);
            }
        }

        public void Dispose()
        {
            foreach (var process in _processes.Values)
            {
                process.Dispose();
            }
        }
    }
}