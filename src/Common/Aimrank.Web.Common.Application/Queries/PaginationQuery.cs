using System;

namespace Aimrank.Web.Common.Application.Queries
{
    public record PaginationQuery
    {
        public int Skip { get; }
        public int Take { get; }

        public PaginationQuery(int? skip, int? take)
        {
            Skip = Math.Max(0, skip ?? 0);
            Take = Math.Max(0, take ?? 0);
        }
    }
}