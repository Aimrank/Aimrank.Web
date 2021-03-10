using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUsers;
using Aimrank.Common.Application.Queries;
using Aimrank.Web.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public record UserSearchDataLoaderInput
    {
        public string Search { get; }
        public PaginationQuery Pagination { get; }

        public UserSearchDataLoaderInput(string search, PaginationQuery pagination)
        {
            Search = search;
            Pagination = pagination;
        }
    }
    
    public class UserSearchDataLoader : DataLoaderBase<UserSearchDataLoaderInput, PaginationDto<User>>
    {
        private readonly IAimrankModule _aimrankModule;
        
        public UserSearchDataLoader(IBatchScheduler batchScheduler, IAimrankModule aimrankModule)
            : base(batchScheduler)
        {
            _aimrankModule = aimrankModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<PaginationDto<User>>>> FetchAsync(IReadOnlyList<UserSearchDataLoaderInput> keys, CancellationToken cancellationToken)
        {
            var result = new List<Result<PaginationDto<User>>>();

            foreach (var input in keys)
            {
                var dto = await _aimrankModule.ExecuteQueryAsync(new GetUsersQuery(input.Search, input.Pagination));

                result.Add(Result<PaginationDto<User>>.Resolve(
                    new PaginationDto<User>(dto.Items.Select(u => new User(u)), dto.Total)));
            }

            return result;
        }
    }
}