using System;

namespace Aimrank.Infrastructure.Application.CSGO
{
    internal record ServerReservation(Guid MatchId, string SteamKey, int Port);
}