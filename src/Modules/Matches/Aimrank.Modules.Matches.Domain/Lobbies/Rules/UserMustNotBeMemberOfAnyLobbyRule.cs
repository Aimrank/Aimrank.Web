using Aimrank.Common.Domain;
using System.Threading.Tasks;
using System;

namespace Aimrank.Modules.Matches.Domain.Lobbies.Rules
{
    public class UserMustNotBeMemberOfAnyLobbyRule : IAsyncBusinessRule
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly Guid _userId;

        public UserMustNotBeMemberOfAnyLobbyRule(ILobbyRepository lobbyRepository, Guid userId)
        {
            _lobbyRepository = lobbyRepository;
            _userId = userId;
        }

        public string Message => "You are already a member of other lobby";
        public string Code => "user_already_other_lobby_member";

        public Task<bool> IsBrokenAsync() => _lobbyRepository.ExistsForMemberAsync(_userId);
    }
}