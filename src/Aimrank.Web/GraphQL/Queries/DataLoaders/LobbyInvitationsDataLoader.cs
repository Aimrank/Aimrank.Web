using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Lobbies.GetLobbyInvitations;
using Aimrank.Web.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public class LobbyInvitationsDataLoader : DataLoaderBase<int, IEnumerable<LobbyInvitation>>
    {
        private readonly IAimrankModule _aimrankModule;
        
        public LobbyInvitationsDataLoader(IBatchScheduler batchScheduler, IAimrankModule aimrankModule)
            : base(batchScheduler)
        {
            _aimrankModule = aimrankModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<IEnumerable<LobbyInvitation>>>> FetchAsync(IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            var result = await _aimrankModule.ExecuteQueryAsync(new GetLobbyInvitationsQuery());

            var invitations = Result<IEnumerable<LobbyInvitation>>.Resolve(result.Select(i => new LobbyInvitation(i)));

            return keys.Select(_ => invitations).ToList();
        }
    }
}