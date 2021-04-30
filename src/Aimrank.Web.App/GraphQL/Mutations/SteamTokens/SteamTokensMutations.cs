using Aimrank.Web.Modules.Matches.Application.Clients;
using HotChocolate.Types;
using HotChocolate;
using System.Threading.Tasks;

namespace Aimrank.Web.App.GraphQL.Mutations.SteamTokens
{
    [ExtendObjectType("Mutation")]
    public class SteamTokensMutations
    {
        private readonly IClusterClient _clusterClient;

        public SteamTokensMutations(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        [AuthorizeRoles("Admin")]
        public async Task<AddSteamTokenPayload> AddSteamToken([GraphQLNonNullType] AddSteamTokenRequest input)
        {
            await _clusterClient.AddSteamTokenAsync(input);
            return new AddSteamTokenPayload();
        }

        [AuthorizeRoles("Admin")]
        public async Task<DeleteSteamTokenPayload> DeleteSteamToken([GraphQLNonNullType] DeleteSteamTokenRequest input)
        {
            await _clusterClient.DeleteSteamTokenAsync(input.Token);
            return new DeleteSteamTokenPayload();
        }
    }
}