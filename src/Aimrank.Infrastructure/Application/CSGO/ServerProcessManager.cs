using Aimrank.Application.CSGO;
using Aimrank.Application.Queries.GetServerProcesses;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Infrastructure.Application.CSGO
{
    internal class ServerProcessManager : IServerProcessManager, IDisposable
    {
        private readonly object _locker = new();
        
        private readonly ConcurrentQueue<int> _availablePorts = new();
        
        private readonly ConcurrentDictionary<Guid, ServerProcess> _processes = new();

        public ServerProcessManager()
        {
            _availablePorts.Enqueue(27016);
            _availablePorts.Enqueue(27017);
            _availablePorts.Enqueue(27018);
            _availablePorts.Enqueue(27019);
        }

        public IEnumerable<ServerProcessDto> GetProcesses()
            => _processes.Values.Select(p => new ServerProcessDto
            {
                ServerId = p.Id,
                Port = p.Configuration.Port
            });

        public void StartServer(Guid serverId, string serverToken, IEnumerable<string> whitelist)
        {
            lock (_locker)
            {
                if (_processes.ContainsKey(serverId))
                {
                    throw new ServerProcessStartException();
                }

                if (_availablePorts.TryDequeue(out var port))
                {
                    var process = new ServerProcess(serverId, new ServerConfiguration(serverToken, port, whitelist.ToList()));

                    if (_processes.TryAdd(serverId, process))
                    {
                        process.Start();
                        return;
                    }
                }
                
                throw new ServerProcessStartException();
            }
        }

        public async Task StopServerAsync(Guid serverId)
        {
            if (_processes.TryRemove(serverId, out var process))
            {
                await process.StopAsync();
                process.Dispose();
                _availablePorts.Enqueue(process.Configuration.Port);
                return;
            }

            throw new ServerProcessStopException();
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