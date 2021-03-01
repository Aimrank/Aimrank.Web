using Aimrank.Application.CSGO;
using Aimrank.Application.Commands.Lobbies.ProcessLobbies;
using Aimrank.Domain.Lobbies;
using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using Aimrank.UnitTests.Mock;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using Xunit;

namespace Aimrank.UnitTests.Application.Commands
{
    public class ProcessLobbiesCommandTests
    {
        #region Setup

        private readonly IServerProcessManager _serverProcessManager = new Mock<IServerProcessManager>().Object;
        private readonly LobbyRepositoryMock _lobbyRepository = new();
        private readonly MatchRepositoryMock _matchRepository = new();
        private readonly UserRepositoryMock _userRepository = new();

        private readonly ProcessLobbiesCommandHandler _handler;

        public ProcessLobbiesCommandTests()
        {
            _handler = new ProcessLobbiesCommandHandler(
                _lobbyRepository,
                _matchRepository,
                _userRepository,
                _serverProcessManager);
        }

        private async Task ArrangeOneVsOneSingleLobby()
        {
            var u1 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user1@mail.com", "user1", _userRepository);
            var u2 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user2@mail.com", "user2", _userRepository);
            
            await _userRepository.AddAsync(u1, "123");
            await _userRepository.AddAsync(u2, "123");
            
            await u1.SetSteamIdAsync("12345678901234567", _userRepository);
            await u2.SetSteamIdAsync("12345678901234568", _userRepository);

            var lobby = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), u1, _lobbyRepository);
            
            _lobbyRepository.Add(lobby);
            
            lobby.Invite(u1, u2);
            
            await lobby.AcceptInvitationAsync(u2, _lobbyRepository);
            
            lobby.StartSearching(u1.Id);
        }
        
        private async Task ArrangeOneVsOneTwoLobbies()
        {
            var u1 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user1@mail.com", "user1", _userRepository);
            var u2 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user2@mail.com", "user2", _userRepository);
            
            await _userRepository.AddAsync(u1, "123");
            await _userRepository.AddAsync(u2, "123");
            
            await u1.SetSteamIdAsync("12345678901234567", _userRepository);
            await u2.SetSteamIdAsync("12345678901234568", _userRepository);

            var lobby1 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), u1, _lobbyRepository);
            var lobby2 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), u2, _lobbyRepository);
            
            _lobbyRepository.Add(lobby1);
            _lobbyRepository.Add(lobby2);
            
            lobby1.StartSearching(u1.Id);
            lobby2.StartSearching(u2.Id);
        }

        private async Task ArrangeOneVsOneFourLobbies()
        {
            var u1 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user1@mail.com", "user1", _userRepository);
            var u2 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user2@mail.com", "user2", _userRepository);
            var u3 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user3@mail.com", "user3", _userRepository);
            var u4 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user4@mail.com", "user4", _userRepository);
            
            await _userRepository.AddAsync(u1, "123");
            await _userRepository.AddAsync(u2, "123");
            await _userRepository.AddAsync(u3, "123");
            await _userRepository.AddAsync(u4, "123");
            
            await u1.SetSteamIdAsync("12345678901234567", _userRepository);
            await u2.SetSteamIdAsync("12345678901234568", _userRepository);
            await u3.SetSteamIdAsync("12345678901234569", _userRepository);
            await u4.SetSteamIdAsync("12345678901234560", _userRepository);

            var lobby1 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), u1, _lobbyRepository);
            var lobby2 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), u2, _lobbyRepository);
            var lobby3 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), u3, _lobbyRepository);
            var lobby4 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), u4, _lobbyRepository);
            
            _lobbyRepository.Add(lobby1);
            _lobbyRepository.Add(lobby2);
            _lobbyRepository.Add(lobby3);
            _lobbyRepository.Add(lobby4);
            
            lobby1.StartSearching(u1.Id);
            lobby2.StartSearching(u2.Id);
            lobby3.StartSearching(u3.Id);
            lobby4.StartSearching(u4.Id);
        }

        private async Task ArrangeTwoVsTwoTwoLobbies()
        {
            var u1 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user1@mail.com", "user1", _userRepository);
            var u2 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user2@mail.com", "user2", _userRepository);
            var u3 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user3@mail.com", "user3", _userRepository);
            var u4 = await User.CreateAsync(new UserId(Guid.NewGuid()), "user4@mail.com", "user4", _userRepository);
            
            await _userRepository.AddAsync(u1, "123");
            await _userRepository.AddAsync(u2, "123");
            await _userRepository.AddAsync(u3, "123");
            await _userRepository.AddAsync(u4, "123");
            
            await u1.SetSteamIdAsync("12345678901234567", _userRepository);
            await u2.SetSteamIdAsync("12345678901234568", _userRepository);
            await u3.SetSteamIdAsync("12345678901234569", _userRepository);
            await u4.SetSteamIdAsync("12345678901234560", _userRepository);

            var lobby1 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), u1, _lobbyRepository);
            var lobby2 = await Lobby.CreateAsync(new LobbyId(Guid.NewGuid()), u3, _lobbyRepository);
            
            _lobbyRepository.Add(lobby1);
            _lobbyRepository.Add(lobby2);
            
            lobby1.ChangeConfiguration(u1.Id, new LobbyConfiguration("lobby1", "aim_map", MatchMode.TwoVsTwo));
            lobby2.ChangeConfiguration(u3.Id, new LobbyConfiguration("lobby2", "aim_map", MatchMode.TwoVsTwo));

            lobby1.Invite(u1, u2);
            lobby2.Invite(u3, u4);
            
            await lobby1.AcceptInvitationAsync(u2, _lobbyRepository);
            await lobby2.AcceptInvitationAsync(u4, _lobbyRepository);
            
            lobby1.StartSearching(u1.Id);
            lobby2.StartSearching(u3.Id);
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
            
            Assert.Equal("12345678901234567", m1.Players.First(p => p.Team == MatchTeam.T).SteamId);
            Assert.Equal("12345678901234568", m1.Players.First(p => p.Team == MatchTeam.CT).SteamId);
            Assert.Equal("12345678901234569", m2.Players.First(p => p.Team == MatchTeam.T).SteamId);
            Assert.Equal("12345678901234560", m2.Players.First(p => p.Team == MatchTeam.CT).SteamId);
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
    }
}