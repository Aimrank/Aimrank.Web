using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.Modules.Matches.Application.Contracts;

namespace Aimrank.Web.Modules.Matches.Application.Matches.GetFinishedMatches
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