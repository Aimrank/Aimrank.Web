using Aimrank.Web.Modules.Cluster.Application.Commands.AddSteamToken;
using Aimrank.Web.Modules.Cluster.Application.Commands.DeleteSteamToken;
using Aimrank.Web.Modules.Cluster.Application.Contracts;
using HotChocolate.Types;
using HotChocolate;
using System.Threading.Tasks;

namespace Aimrank.Web.App.GraphQL.Mutations.SteamTokens
{
    [ExtendObjectType("Mutation")]
    public class SteamTokensMutations
    {
        private readonly IClusterModule _clusterModule;

        public SteamTokensMutations(IClusterModule clusterModule)
        {
            _clusterModule = clusterModule;
        }

        [AuthorizeRoles("Admin")]
        public async Task<AddSteamTokenPayload> AddSteamToken([GraphQLNonNullType] AddSteamTokenCommand input)
        {
            await _clusterModule.ExecuteCommandAsync(input);
            return new AddSteamTokenPayload();
        }

        [AuthorizeRoles("Admin")]
        public async Task<DeleteSteamTokenPayload> DeleteSteamToken([GraphQLNonNullType] DeleteSteamTokenCommand input)
        {
            await _clusterModule.ExecuteCommandAsync(input);
            return new DeleteSteamTokenPayload();
        }
    }
}