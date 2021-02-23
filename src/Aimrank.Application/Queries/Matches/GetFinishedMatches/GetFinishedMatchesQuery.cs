using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Queries;
using System;

namespace Aimrank.Application.Queries.Matches.GetFinishedMatches
{
    public class GetFinishedMatchesQuery : IQuery<PaginationDto<MatchDto>>
    {
        public Guid UserId { get; }
        public FinishedMatchesFilter Filter { get; }
        public PaginationQuery Pagination { get; }

        public GetFinishedMatchesQuery(Guid userId, FinishedMatchesFilter filter, PaginationQuery pagination)
        {
            UserId = userId;
            Filter = filter;
            Pagination = new PaginationQuery(pagination.Page, Math.Min(pagination.Size, 100));
        }
    }
}