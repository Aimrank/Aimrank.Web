using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Queries;

namespace Aimrank.Application.Queries.Matches.GetFinishedMatches
{
    public class GetFinishedMatchesQuery : IQuery<PaginationDto<MatchDto>>
    {
        public FinishedMatchesFilter Filter { get; }
        public PaginationQuery Pagination { get; }

        public GetFinishedMatchesQuery(FinishedMatchesFilter filter, PaginationQuery pagination)
        {
            Filter = filter;
            Pagination = pagination;
        }
    }
}