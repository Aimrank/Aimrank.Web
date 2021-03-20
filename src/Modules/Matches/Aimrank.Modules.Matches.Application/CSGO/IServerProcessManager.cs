using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aimrank.Modules.Matches.Application.CSGO
{
    public interface IServerProcessManager
    {
        void CreateReservation(Guid matchId);
        void DeleteReservation(Guid matchId);
        string StartServer(Guid matchId, string map, IEnumerable<string> whitelist);
        void StopServer(Guid matchId);
        Task ExecuteCommandAsync(Guid matchId, string command);
    }
}