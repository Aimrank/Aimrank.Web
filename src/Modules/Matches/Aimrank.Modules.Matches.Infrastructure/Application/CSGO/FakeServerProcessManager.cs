using Aimrank.Modules.Matches.Application.CSGO;
using Aimrank.Modules.Matches.Infrastructure.Configuration.CSGO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Modules.Matches.Infrastructure.Application.CSGO
{
    internal class FakeServerProcessManager : IServerProcessManager
    {
        private readonly object _locker = new();
        
        private readonly ConcurrentQueue<int> _availablePorts = new();
        
        private readonly ConcurrentDictionary<Guid, ServerConfiguration> _processes = new();
        
        private readonly ConcurrentDictionary<Guid, ServerReservation> _reservations = new();
        
        private readonly CSGOSettings _csgoSettings;
        
        public FakeServerProcessManager(CSGOSettings csgoSettings)
        {
            _csgoSettings = csgoSettings;
            _availablePorts.Enqueue(27016);
            _availablePorts.Enqueue(27017);
            _availablePorts.Enqueue(27018);
            _availablePorts.Enqueue(27019);
        }

        public bool TryCreateReservation(Guid matchId)
        {
            lock (_locker)
            {
                if (_processes.ContainsKey(matchId))
                {
                    return false;
                }

                if (!_availablePorts.TryDequeue(out var port))
                {
                    return false;
                }

                var steamKey = GetUnusedSteamKey();

                var reservation = new ServerReservation(matchId, steamKey, port);

                _reservations.TryAdd(reservation.MatchId, reservation);

                return true;
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
                    var configuration = new ServerConfiguration(
                        reservation.SteamKey,
                        reservation.Port,
                        whitelist.ToList(),
                        map);

                    if (_processes.TryAdd(reservation.MatchId, configuration))
                    {
                        return "localhost";
                    }
                }

                throw new ServerProcessStartException();
            }
        }

        public void StopServer(Guid matchId)
        {
            if (_processes.TryRemove(matchId, out var process))
            {
                lock (_locker)
                {
                    _availablePorts.Enqueue(process.Port);
                }
            }
        }

        public Task ExecuteCommandAsync(Guid matchId, string command) => Task.CompletedTask;
        
        private string GetUnusedSteamKey()
        {
            var steamKey =
                _csgoSettings.SteamKeys.FirstOrDefault(k =>
                    _processes.Values.All(p => p.SteamKey != k) &&
                    _reservations.Values.All(r => r.SteamKey != k));

            if (steamKey is null)
            {
                throw new ServerReservationException();
            }

            return steamKey;
        }
    }
}