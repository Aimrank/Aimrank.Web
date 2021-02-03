using System.Collections.Generic;
using System;

namespace Aimrank.Application.CSGO.Commands.FinishMatch
{
    public class FinishMatchCommand : IServerEventCommand
    {
        public Guid ServerId { get; set; }
        public MatchEndEventTeam TeamTerrorists { get; }
        public MatchEndEventTeam TeamCounterTerrorists { get; }

        public FinishMatchCommand(
            Guid serverId,
            MatchEndEventTeam teamTerrorists,
            MatchEndEventTeam teamCounterTerrorists)
        {
            ServerId = serverId;
            TeamTerrorists = teamTerrorists;
            TeamCounterTerrorists = teamCounterTerrorists;
        }

        public record MatchEndEventTeam(int Score, IEnumerable<MatchEndEventPlayer> Clients);
        public record MatchEndEventPlayer(string SteamId, string Name, int Kills, int Assists, int Deaths, int Score);
    }
}