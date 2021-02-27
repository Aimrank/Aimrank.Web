using System.Collections.Generic;
using System;

namespace Aimrank.Application.CSGO.Commands.FinishMatch
{
    public class FinishMatchCommand : IServerEventCommand
    {
        public Guid MatchId { get; set; }
        public int Winner { get; }
        public MatchEndEventTeam TeamTerrorists { get; }
        public MatchEndEventTeam TeamCounterTerrorists { get; }

        public FinishMatchCommand(
            Guid matchId,
            int winner,
            MatchEndEventTeam teamTerrorists,
            MatchEndEventTeam teamCounterTerrorists)
        {
            MatchId = matchId;
            Winner = winner;
            TeamTerrorists = teamTerrorists;
            TeamCounterTerrorists = teamCounterTerrorists;
        }

        public record MatchEndEventTeam(int Score, IEnumerable<MatchEndEventPlayer> Clients);
        public record MatchEndEventPlayer(string SteamId, string Name, int Kills, int Assists, int Deaths, int Score, int Hs);
    }
}