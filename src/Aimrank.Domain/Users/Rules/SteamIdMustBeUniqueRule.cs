using Aimrank.Common.Domain;
using System.Threading.Tasks;

namespace Aimrank.Domain.Users.Rules
{
    public class SteamIdMustBeUniqueRule : IAsyncBusinessRule
    {
        private readonly IUserRepository _userRepository;
        private readonly string _steamId;
        private readonly UserId _userId;

        public SteamIdMustBeUniqueRule(IUserRepository userRepository, string steamId, UserId userId)
        {
            _userRepository = userRepository;
            _steamId = steamId;
            _userId = userId;
        }

        public string Message => "This SteamID is already assigned to different account";
        public string Code => "steam_id_not_unique";

        public Task<bool> IsBrokenAsync() => _userRepository.ExistsSteamIdAsync(_steamId, _userId);
    }
}