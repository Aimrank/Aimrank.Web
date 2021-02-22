using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Queries;
using System;

namespace Aimrank.Application.Queries.Matches.GetMatchesHistory
{
    public class GetMatchesHistoryQuery : IQuery<PaginationDto<MatchHistoryDto>>
    {
        public Guid UserId { get; }
        public MatchHistoryFilter Filter { get; }
        public PaginationQuery Pagination { get; }

        public GetMatchesHistoryQuery(Guid userId, MatchHistoryFilter filter, PaginationQuery pagination)
        {
            UserId = userId;
            Filter = filter;
            Pagination = new PaginationQuery(pagination.Page, Math.Min(pagination.Size, 100));
        }
    }
}