using Aimrank.Modules.Matches.Application.CSGO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System;
using Aimrank.Modules.Matches.Infrastructure.Configuration.CSGO;

namespace Aimrank.Modules.Matches.Infrastructure.Application.CSGO
{
    internal class ServerProcessManager : IServerProcessManager, IDisposable
    {
        private readonly object _locker = new();
        
        private readonly ConcurrentQueue<int> _availablePorts = new();
        
        private readonly ConcurrentDictionary<Guid, ServerProcess> _processes = new();

        private readonly ConcurrentDictionary<Guid, ServerReservation> _reservations = new();

        private readonly CSGOSettings _csgoSettings;

        public ServerProcessManager(CSGOSettings csgoSettings)
        {
            _csgoSettings = csgoSettings;
            _availablePorts.Enqueue(27016);
            _availablePorts.Enqueue(27017);
            _availablePorts.Enqueue(27018);
            _availablePorts.Enqueue(27019);
        }

        public void CreateReservation(Guid matchId)
        {
            lock (_locker)
            {
                if (_processes.ContainsKey(matchId))
                {
                    throw new ServerReservationException();
                }

                if (!_availablePorts.TryDequeue(out var port))
                {
                    throw new ServerReservationException();
                }
                
                var steamKey = GetUnusedSteamKey();

                var reservation = new ServerReservation(matchId, steamKey, port);

                _reservations.TryAdd(reservation.MatchId, reservation);
            }
        }

        public void DeleteReservation(Guid matchId)
        {
            lock (_locker)
            {
                if (_reservations.TryRemove(matchId, out var reservation))
                {
                    _availablePorts.Enqueue(reservation.Port);
                }
            }
        }

        public string StartServer(Guid matchId, string map, IEnumerable<string> whitelist)
        {
            lock (_locker)
            {
                if (_reservations.TryRemove(matchId, out var reservation))
                {
                    var process = new ServerProcess(reservation.MatchId, new ServerConfiguration(
                        reservation.SteamKey, reservation.Port, whitelist.ToList(), map));

                    if (_processes.TryAdd(reservation.MatchId, process))
                    {
                        process.Start();

                        return $"{GetLocalIpAddress()}:{reservation.Port}";
                    }
                }

                throw new ServerProcessStartException();
            }
        }

        public void StopServer(Guid matchId)
        {
            if (_processes.TryRemove(matchId, out var process))
            {
                Task.Run(async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(20));
                    await process.StopAsync();
                    process.Dispose();
                    
                    lock (_locker)
                    {
                        _availablePorts.Enqueue(process.Configuration.Port);
                    }
                });
            }
        }

        public async Task ExecuteCommandAsync(Guid matchId, string command)
        {
            if (_processes.TryGetValue(matchId, out var process))
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
        
        private string GetUnusedSteamKey()
        {
            var steamKey =
                _csgoSettings.SteamKeys.FirstOrDefault(k =>
                    _processes.Values.All(p => p.Configuration.SteamKey != k) &&
                    _reservations.Values.All(r => r.SteamKey != k));

            if (steamKey is null)
            {
                throw new ServerReservationException();
            }

            return steamKey;
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