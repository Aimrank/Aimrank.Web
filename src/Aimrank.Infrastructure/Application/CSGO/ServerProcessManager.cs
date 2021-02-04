using Aimrank.Application.CSGO;
using Aimrank.Infrastructure.Configuration.CSGO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Aimrank.Infrastructure.Application.CSGO
{
    internal class ServerProcessManager : IServerProcessManager, IDisposable
    {
        private readonly object _locker = new();
        
        private readonly ConcurrentQueue<int> _availablePorts = new();
        
        private readonly ConcurrentDictionary<Guid, ServerProcess> _processes = new();

        private readonly CSGOSettings _csgoSettings;

        public ServerProcessManager(CSGOSettings csgoSettings)
        {
            _csgoSettings = csgoSettings;
            _availablePorts.Enqueue(27016);
            _availablePorts.Enqueue(27017);
            _availablePorts.Enqueue(27018);
            _availablePorts.Enqueue(27019);
        }

        public string StartServer(Guid serverId, IEnumerable<string> whitelist, string map)
        {
            lock (_locker)
            {
                if (_processes.ContainsKey(serverId))
                {
                    throw new ServerProcessStartException();
                }

                if (_availablePorts.TryDequeue(out var port))
                {
                    var steamKey =
                        _csgoSettings.SteamKeys.FirstOrDefault(k =>
                            _processes.Values.All(p => p.Configuration.Token != k));

                    if (steamKey is null)
                    {
                        throw new ServerProcessStartException();
                    }
                    
                    var process = new ServerProcess(serverId, new ServerConfiguration(steamKey, port, whitelist.ToList(), map));

                    if (_processes.TryAdd(serverId, process))
                    {
                        process.Start();

                        return $"{GetLocalIpAddress()}:{port}";
                    }
                }
                
                throw new ServerProcessStartException();
            }
        }

        public void StopServer(Guid serverId)
        {
            if (_processes.TryRemove(serverId, out var process))
            {
                Task.Run(async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(20));
                    await process.StopAsync();
                    process.Dispose();
                    _availablePorts.Enqueue(process.Configuration.Port);
                });
                
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

        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "localhost";
        }
    }
}