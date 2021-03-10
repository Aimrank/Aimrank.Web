using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Matches.GetFinishedMatches;
using Aimrank.Common.Application.Queries;
using Aimrank.Web.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public record MatchesDataLoaderInput
    {
        public FinishedMatchesFilter Filter { get; }
        public PaginationQuery Pagination { get; }

        public MatchesDataLoaderInput(FinishedMatchesFilter filter, PaginationQuery pagination)
        {
            Filter = filter;
            Pagination = pagination;
        }
    }
    
    public class MatchesDataLoader : DataLoaderBase<MatchesDataLoaderInput, PaginationDto<Match>>
    {
        private readonly IAimrankModule _aimrankModule;
        
        public MatchesDataLoader(IBatchScheduler batchScheduler, IAimrankModule aimrankModule)
            : base(batchScheduler)
        {
            _aimrankModule = aimrankModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<PaginationDto<Match>>>> FetchAsync(
            IReadOnlyList<MatchesDataLoaderInput> keys, CancellationToken cancellationToken)
        {
            var result = new List<Result<PaginationDto<Match>>>();

            foreach (var input in keys)
            {
                var dto = await _aimrankModule.ExecuteQueryAsync(
                    new GetFinishedMatchesQuery(input.Filter, input.Pagination));
                
                result.Add(Result<PaginationDto<Match>>.Resolve(
                    new PaginationDto<Match>(dto.Items.Select(m => new Match(m)), dto.Total)));
            }

            return result;
        }
    }
}