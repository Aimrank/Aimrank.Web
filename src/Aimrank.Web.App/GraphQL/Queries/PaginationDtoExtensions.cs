using Aimrank.Web.Common.Application.Queries;
using HotChocolate.Types.Pagination;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.App.GraphQL.Queries
{
    public static class PaginationDtoExtensions
    {
        public static Connection<T> AsConnection<T>(this PaginationDto<T> paginationDto, int? skip, int? take)
        {
            var skipCount = Math.Max(skip ?? 0, 0);
            var takeCount = Math.Max(take ?? 0, 0);

            var edges = new ReadOnlyCollection<Edge<T>>(
                paginationDto.Items
                    .Select((item, index) => new Edge<T>(item, (index + skipCount).ToString()))
                    .ToList());
            

            var info = new ConnectionPageInfo(
                skipCount + takeCount < paginationDto.Total,
                skipCount > 0,
                edges.Count == 0 ? null : skipCount.ToString(),
                edges.Count == 0 ? null : (skipCount + edges.Count - 1).ToString(),
                paginationDto.Total);

            return new Connection<T>(edges, info, _ => new ValueTask<int>(paginationDto.Total));
        }
    }
}