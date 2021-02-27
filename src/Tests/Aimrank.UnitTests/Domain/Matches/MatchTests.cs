using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using System.Linq;
using System;
using Xunit;

namespace Aimrank.UnitTests.Domain.Matches
{
    public class MatchTests
    {
        #region Setup

        private const string P1Steam = "12345678901234567";
        private const string P2Steam = "12345678901234568";
        
        private readonly Match _match;

        public MatchTests()
        {
            _match = new Match(new MatchId(Guid.NewGuid()), "aim_map", MatchMode.OneVsOne, new[]
            {
                new LobbyId(Guid.NewGuid()),
                new LobbyId(Guid.NewGuid())
            });
            
            _match.AddPlayer(new UserId(Guid.NewGuid()), P1Steam, MatchTeam.T);
            _match.AddPlayer(new UserId(Guid.NewGuid()), P2Steam, MatchTeam.CT);
        }

        private MatchPlayer P1 => _match?.Players.FirstOrDefault(p => p.SteamId == P1Steam);
        private MatchPlayer P2 => _match?.Players.FirstOrDefault(p => p.SteamId == P2Steam);

        #endregion
        
        [Fact]
        public void Finish_UpdatesPlayersRating_WhenTerroristsWin()
        {

            _match.UpdateScore(MatchWinner.T, 16, 0);
            _match.UpdatePlayerStats(P1Steam, new MatchPlayerStats(16, 0, 0, 32, 0));
            _match.UpdatePlayerStats(P2Steam, new MatchPlayerStats(0, 0, 16, 0, 0));
            
            _match.Finish();
            
            Assert.True(P1.RatingStart < P1.RatingEnd);
            Assert.True(P2.RatingStart > P2.RatingEnd);
        }
        
        [Fact]
        public void Finish_UpdatesPlayersRating_WhenCounterTerroristsWin()
        {

            _match.UpdateScore(MatchWinner.CT, 0, 16);
            _match.UpdatePlayerStats(P1Steam, new MatchPlayerStats(0, 0, 16, 0, 0));
            _match.UpdatePlayerStats(P2Steam, new MatchPlayerStats(16, 0, 0, 32, 0));
            
            _match.Finish();
            
            Assert.True(P1.RatingStart > P1.RatingEnd);
            Assert.True(P2.RatingStart < P2.RatingEnd);
        }
        
        [Fact]
        public void Finish_UpdatesPlayersRating_WhenDraw()
        {

            _match.UpdateScore(MatchWinner.Draw, 15, 15);
            _match.UpdatePlayerStats(P1Steam, new MatchPlayerStats(15, 0, 15, 30, 0));
            _match.UpdatePlayerStats(P2Steam, new MatchPlayerStats(15, 0, 15, 30, 0));
            
            _match.Finish();
            
            Assert.True(P1.RatingStart == P1.RatingEnd);
            Assert.True(P2.RatingStart == P2.RatingEnd);
        }
    }
}