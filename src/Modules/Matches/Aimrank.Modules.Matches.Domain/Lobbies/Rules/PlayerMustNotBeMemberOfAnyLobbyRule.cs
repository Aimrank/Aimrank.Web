using Aimrank.Common.Domain;
using Aimrank.Modules.Matches.Domain.Players;
using System.Threading.Tasks;

namespace Aimrank.Modules.Matches.Domain.Lobbies.Rules
{
    public class PlayerMustNotBeMemberOfAnyLobbyRule : IAsyncBusinessRule
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly PlayerId _playerId;

        public PlayerMustNotBeMemberOfAnyLobbyRule(ILobbyRepository lobbyRepository, PlayerId playerId)
        {
            _lobbyRepository = lobbyRepository;
            _playerId = playerId;
        }

        public string Message => "You are already a member of other lobby";
        public string Code => "player_already_other_lobby_member";

        public Task<bool> IsBrokenAsync() => _lobbyRepository.ExistsForMemberAsync(_playerId);
    }
}