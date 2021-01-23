using Aimrank.Web.Events;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Server
{
    public class ServerProcessManager : IDisposable
    {
        private readonly object _locker = new();
        
        private readonly EventBus _eventBus;
        
        private readonly ConcurrentQueue<int> _availablePorts = new();
        
        private readonly ConcurrentDictionary<Guid, ServerProcess> _processes = new();

        public ServerProcessManager(EventBus eventBus)
        {
            _eventBus = eventBus;
            _availablePorts.Enqueue(27016);
            _availablePorts.Enqueue(27017);
            _availablePorts.Enqueue(27018);
            _availablePorts.Enqueue(27019);
        }

        public IEnumerable<ServerProcess> GetProcesses() => _processes.Values;

        public bool StartServer(Guid serverId, string serverToken, IEnumerable<string> whitelist)
        {
            lock (_locker)
            {
                if (_processes.ContainsKey(serverId))
                {
                    return false;
                }

                if (_availablePorts.TryDequeue(out var port))
                {
                    var process = new ServerProcess(serverId, new ServerConfiguration(serverToken, port, whitelist.ToList()));
                    
                    process.EventReceived += (_, ea) =>
                    {
                        _eventBus.PublishAsync(serverId, ea.Content);
                    };

                    if (_processes.TryAdd(serverId, process))
                    {
                        process.Start();
                        return true;
                    }
                }

                return false;
            }
        }

        public async Task<bool> StopServerAsync(Guid serverId)
        {
            if (_processes.TryRemove(serverId, out var process))
            {
                await process.StopAsync();
                process.Dispose();
                _availablePorts.Enqueue(process.Configuration.Port);
                return true;
            }

            return false;
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