using System.Collections.Generic;

namespace Aimrank.Web.Common.Application.Queries
{
    public class PaginationDto<T>
    {
        public IEnumerable<T> Items { get; }
        public int Total { get; }

        public PaginationDto(IEnumerable<T> items, int total)
        {
            Items = items;
            Total = total;
        }
    }
}