using Aimrank.Web.Common.Domain;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Matches.Domain.Players.Rules
{
    public class SteamIdMustBeUniqueRule : IAsyncBusinessRule
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly PlayerId _playerId;
        private readonly string _steamId;

        public SteamIdMustBeUniqueRule(IPlayerRepository playerRepository, PlayerId playerId, string steamId)
        {
            _playerRepository = playerRepository;
            _playerId = playerId;
            _steamId = steamId;
        }

        public string Message => "This SteamID is already assigned to different account";
        public string Code => "steam_id_not_unique";

        public Task<bool> IsBrokenAsync() => _playerRepository.ExistsSteamIdAsync(_playerId, _steamId);
    }
}