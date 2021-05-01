using Aimrank.Web.App.GraphQL.Queries.Models;
using Aimrank.Web.Modules.Matches.Application.Clients;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.GraphQL.Queries.DataLoaders
{
    public class SteamTokensDataLoader : DataLoaderBase<int, IEnumerable<SteamToken>>
    {
        private readonly IClusterClient _clusterClient;
        
        public SteamTokensDataLoader(IBatchScheduler batchScheduler, IClusterClient clusterClient)
            : base(batchScheduler)
        {
            _clusterClient = clusterClient;
        }

        protected override async ValueTask<IReadOnlyList<Result<IEnumerable<SteamToken>>>> FetchAsync(
            IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            var result = await _clusterClient.GetSteamTokensAsync();

            var steamTokens = Result<IEnumerable<SteamToken>>.Resolve(result.Select(t => new SteamToken(t)));
            
            return keys.Select(_ => steamTokens).ToList();
        }
    }
}