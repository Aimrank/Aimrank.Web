using System;

namespace Aimrank.Modules.Matches.Infrastructure.Application.CSGO
{
    internal record ServerReservation(Guid MatchId, string SteamKey, int Port);
}