using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.Matches.Domain.Players.Rules;
using System.Threading.Tasks;

namespace Aimrank.Web.Modules.Matches.Domain.Players
{
    public class Player : Entity, IAggregateRoot
    {
        public PlayerId Id { get; }
        public string SteamId { get; private set; }

        private Player() {}

        private Player(PlayerId id, string steamId)
        {
            Id = id;
            SteamId = steamId;
        }

        public static async Task<Player> CreateAsync(PlayerId id, string steamId, IPlayerRepository playerRepository)
        {
            BusinessRules.Check(new SteamIdMustBeValidRule(steamId));
            await BusinessRules.CheckAsync(new SteamIdMustBeUniqueRule(playerRepository, id, steamId));
            
            return new Player(id, steamId);
        }

        public async Task SetSteamIdAsync(string steamId, IPlayerRepository playerRepository)
        {
            BusinessRules.Check(new SteamIdMustBeValidRule(steamId));
            await BusinessRules.CheckAsync(new SteamIdMustBeUniqueRule(playerRepository, Id, steamId));

            SteamId = steamId;
        }
    }
}