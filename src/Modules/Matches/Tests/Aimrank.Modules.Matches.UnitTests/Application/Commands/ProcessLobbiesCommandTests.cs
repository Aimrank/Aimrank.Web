using Aimrank.Modules.Matches.Application.CSGO;
using Aimrank.Modules.Matches.Application.Lobbies.ProcessLobbies;
using Aimrank.Modules.Matches.Domain.Lobbies;
using Aimrank.Modules.Matches.Domain.Matches;
using Aimrank.Modules.Matches.Domain.Players;
using Aimrank.Modules.Matches.UnitTests.Mock;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using Xunit;

namespace Aimrank.Modules.Matches.UnitTests.Application.Commands
{
    public class ProcessLobbiesCommandTests
    {
        #region Setup
        
        private readonly LobbyRepositoryMock _lobbyRepository = new();
        private readonly MatchRepositoryMock _matchRepository = new();
        private readonly PlayerRepositoryMock _playerRepository = new();

        private readonly ProcessLobbiesCommandHandler _handler;

        public ProcessLobbiesCommandTests()
        {
            var mock = new Mock<IServerProcessManager>();
            
            mock
                .Setup(m => m.TryCreateReservation(It.IsAny<Guid>()))
                .Returns(true);
            
            _handler = new ProcessLobbiesCommandHandler(
                _lobbyRepository,
                _matchRepository,
                _playerRepository,
                mock.Object);
        }

        private async Task ArrangeOneVsOneSingleLobby()
        {
            var p1 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234567", _playerRepository);
            var p2 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234568", _playerRepository);
            
            _playerRepository.Add(p1);
            _playerRepository.Add(p2);
            
            var lobby = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p1, _lobbyRepository);
            
            _lobbyRepository.Add(lobby);
            
            lobby.Invite(p1, p2);
            
            await lobby.AcceptInvitationAsync(p2, _lobbyRepository);
            
            lobby.StartSearching(p1.Id);
        }
        
        private async Task ArrangeOneVsOneTwoLobbies()
        {
            var p1 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234567", _playerRepository);
            var p2 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234568", _playerRepository);
            
            _playerRepository.Add(p1);
            _playerRepository.Add(p2);

            var lobby1 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p1, _lobbyRepository);
            var lobby2 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p2, _lobbyRepository);
            
            _lobbyRepository.Add(lobby1);
            _lobbyRepository.Add(lobby2);
            
            lobby1.StartSearching(p1.Id);
            lobby2.StartSearching(p2.Id);
        }

        private async Task ArrangeOneVsOneFourLobbies()
        {
            var p1 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234567", _playerRepository);
            var p2 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234568", _playerRepository);
            var p3 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234569", _playerRepository);
            var p4 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234560", _playerRepository);
            
            _playerRepository.Add(p1);
            _playerRepository.Add(p2);
            _playerRepository.Add(p3);
            _playerRepository.Add(p4);
            
            var lobby1 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p1, _lobbyRepository);
            var lobby2 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p2, _lobbyRepository);
            var lobby3 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p3, _lobbyRepository);
            var lobby4 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p4, _lobbyRepository);
            
            _lobbyRepository.Add(lobby1);
            _lobbyRepository.Add(lobby2);
            _lobbyRepository.Add(lobby3);
            _lobbyRepository.Add(lobby4);
            
            lobby1.StartSearching(p1.Id);
            lobby2.StartSearching(p2.Id);
            lobby3.StartSearching(p3.Id);
            lobby4.StartSearching(p4.Id);
        }

        private async Task ArrangeTwoVsTwoTwoLobbies()
        {
            var p1 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234567", _playerRepository);
            var p2 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234568", _playerRepository);
            var p3 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234569", _playerRepository);
            var p4 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234560", _playerRepository);
            
            _playerRepository.Add(p1);
            _playerRepository.Add(p2);
            _playerRepository.Add(p3);
            _playerRepository.Add(p4);

            var lobby1 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p1, _lobbyRepository);
            var lobby2 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p3, _lobbyRepository);
            
            _lobbyRepository.Add(lobby1);
            _lobbyRepository.Add(lobby2);
            
            lobby1.ChangeConfiguration(p1.Id, new LobbyConfiguration("lobby1", MatchMode.TwoVsTwo, new []{"aim_map"}));
            lobby2.ChangeConfiguration(p3.Id, new LobbyConfiguration("lobby2", MatchMode.TwoVsTwo, new []{"aim_map"}));

            lobby1.Invite(p1, p2);
            lobby2.Invite(p3, p4);
            
            await lobby1.AcceptInvitationAsync(p2, _lobbyRepository);
            await lobby2.AcceptInvitationAsync(p4, _lobbyRepository);
            
            lobby1.StartSearching(p1.Id);
            lobby2.StartSearching(p3.Id);
        }
        
        private async Task ArrangeOneVsOneFourLobbiesTwoMaps()
        {
            var p1 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234567", _playerRepository);
            var p2 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234568", _playerRepository);
            var p3 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234569", _playerRepository);
            var p4 = await Player.CreateAsync(new PlayerId(Guid.NewGuid()), "12345678901234560", _playerRepository);
            
            _playerRepository.Add(p1);
            _playerRepository.Add(p2);
            _playerRepository.Add(p3);
            _playerRepository.Add(p4);

            var lobby1 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p1, _lobbyRepository);
            var lobby2 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p2, _lobbyRepository);
            var lobby3 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p3, _lobbyRepository);
            var lobby4 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), "name", p4, _lobbyRepository);
            
            _lobbyRepository.Add(lobby1);
            _lobbyRepository.Add(lobby2);
            _lobbyRepository.Add(lobby3);
            _lobbyRepository.Add(lobby4);
            
            lobby1.ChangeConfiguration(p1.Id, new LobbyConfiguration("lobby1", MatchMode.OneVsOne, new []{"aim_map"}));
            lobby2.ChangeConfiguration(p2.Id, new LobbyConfiguration("lobby2", MatchMode.OneVsOne, new []{"am_redline_14"}));
            lobby3.ChangeConfiguration(p3.Id, new LobbyConfiguration("lobby3", MatchMode.OneVsOne, new []{"am_redline_14"}));
            lobby4.ChangeConfiguration(p4.Id, new LobbyConfiguration("lobby4", MatchMode.OneVsOne, new[] {"aim_map"}));
            
            lobby1.StartSearching(p1.Id);
            lobby2.StartSearching(p2.Id);
            lobby3.StartSearching(p3.Id);
            lobby4.StartSearching(p4.Id);
        }

        #endregion

        [Fact]
        public async Task Processes_Lobbies_When_OneVsOne_SingleLobby()
        {
            await ArrangeOneVsOneSingleLobby();

            await _handler.Handle(new ProcessLobbiesCommand(), CancellationToken.None);

            var match = _matchRepository.Matches.First();

            Assert.Single(_matchRepository.Matches);
            Assert.Single(match.Players.Where(p => p.Team == MatchTeam.T));
            Assert.Single(match.Players.Where(p => p.Team == MatchTeam.CT));
        }

        [Fact]
        public async Task Processes_Lobbies_When_OneVsOne_TwoLobbies()
        {
            await ArrangeOneVsOneTwoLobbies();

            await _handler.Handle(new ProcessLobbiesCommand(), CancellationToken.None);

            var match = _matchRepository.Matches.First();

            Assert.Single(_matchRepository.Matches);
            Assert.Single(match.Players.Where(p => p.Team == MatchTeam.T));
            Assert.Single(match.Players.Where(p => p.Team == MatchTeam.CT));
        }

        [Fact]
        public async Task Processes_Lobbies_When_OneVsOne_FourLobbies()
        {
            await ArrangeOneVsOneFourLobbies();
            
            await _handler.Handle(new ProcessLobbiesCommand(), CancellationToken.None);
            
            Assert.Equal(2, _matchRepository.Matches.Count());

            var m1 = _matchRepository.Matches.ElementAt(0);
            var m2 = _matchRepository.Matches.ElementAt(1);
            
            Assert.Single(m1.Players.Where(p => p.Team == MatchTeam.T));
            Assert.Single(m1.Players.Where(p => p.Team == MatchTeam.CT));
            Assert.Single(m2.Players.Where(p => p.Team == MatchTeam.T));
            Assert.Single(m2.Players.Where(p => p.Team == MatchTeam.CT));
            
            Assert.Equal("12345678901234567", m1.Players.First(p => p.Team == MatchTeam.CT).SteamId);
            Assert.Equal("12345678901234568", m1.Players.First(p => p.Team == MatchTeam.T).SteamId);
            Assert.Equal("12345678901234569", m2.Players.First(p => p.Team == MatchTeam.CT).SteamId);
            Assert.Equal("12345678901234560", m2.Players.First(p => p.Team == MatchTeam.T).SteamId);
        }

        [Fact]
        public async Task Process_Lobbies_When_TwoVsTwo_TwoLobbies()
        {
            await ArrangeTwoVsTwoTwoLobbies();

            await _handler.Handle(new ProcessLobbiesCommand(), CancellationToken.None);

            var match = _matchRepository.Matches.First();

            Assert.Single(_matchRepository.Matches);
            
            var teamT = match.Players.Where(p => p.Team == MatchTeam.T).ToArray();
            var teamCT = match.Players.Where(p => p.Team == MatchTeam.CT).ToArray();

            Assert.Equal(2, teamT.Length);
            Assert.Equal(2, teamCT.Length);
            
            Assert.Equal("12345678901234567", teamT[0].SteamId);
            Assert.Equal("12345678901234568", teamT[1].SteamId);
            Assert.Equal("12345678901234569", teamCT[0].SteamId);
            Assert.Equal("12345678901234560", teamCT[1].SteamId);
        }

        [Fact]
        public async Task Process_Lobbies_When_OneVsOne_FourLobbies_TwoMaps()
        {
            await ArrangeOneVsOneFourLobbiesTwoMaps();

            await _handler.Handle(new ProcessLobbiesCommand(), CancellationToken.None);

            var matches = _matchRepository.Matches.ToList();
            
            Assert.Equal(2, matches.Count);

            var match1 = matches[0];
            var match2 = matches[1];

            Assert.True(match1.Players.All(p => p.SteamId == "12345678901234568" || p.SteamId == "12345678901234569"));
            Assert.True(match2.Players.All(p => p.SteamId == "12345678901234567" || p.SteamId == "12345678901234560"));
        }
    }
}