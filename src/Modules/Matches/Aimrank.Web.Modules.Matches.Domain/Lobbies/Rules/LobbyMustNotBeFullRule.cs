using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies.Rules
{
    public class LobbyMustNotBeFullRule : IBusinessRule
    {
        private readonly Lobby _lobby;

        public LobbyMustNotBeFullRule(Lobby lobby)
        {
            _lobby = lobby;
        }

        public string Message => "Lobby is full";
        public string Code => "lobby_is_full";

        public bool IsBroken() => _lobby.IsFull();
    }
}