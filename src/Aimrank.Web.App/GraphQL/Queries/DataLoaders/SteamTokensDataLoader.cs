using Aimrank.Web.App.GraphQL.Queries.Models;
using Aimrank.Web.Modules.Cluster.Application.Contracts;
using Aimrank.Web.Modules.Cluster.Application.Queries.GetSteamTokens;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.GraphQL.Queries.DataLoaders
{
    public class SteamTokensDataLoader : DataLoaderBase<int, IEnumerable<SteamToken>>
    {
        private readonly IClusterModule _clusterModule;
        
        public SteamTokensDataLoader(IBatchScheduler batchScheduler, IClusterModule clusterModule)
            : base(batchScheduler)
        {
            _clusterModule = clusterModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<IEnumerable<SteamToken>>>> FetchAsync(
            IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            var result = await _clusterModule.ExecuteQueryAsync(new GetSteamTokensQuery());

            var steamTokens = Result<IEnumerable<SteamToken>>.Resolve(result.Select(t => new SteamToken(t)));
            
            return keys.Select(_ => steamTokens).ToList();
        }
    }
}