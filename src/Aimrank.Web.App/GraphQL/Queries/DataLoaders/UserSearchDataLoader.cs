using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Users.GetUsers;
using Aimrank.Web.App.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.App.GraphQL.Queries.DataLoaders
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
        private readonly IUserAccessModule _userAccessModule;
        
        public UserSearchDataLoader(IBatchScheduler batchScheduler, IUserAccessModule userAccessModule)
            : base(batchScheduler)
        {
            _userAccessModule = userAccessModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<PaginationDto<User>>>> FetchAsync(IReadOnlyList<UserSearchDataLoaderInput> keys, CancellationToken cancellationToken)
        {
            var result = new List<Result<PaginationDto<User>>>();

            foreach (var input in keys)
            {
                var dto = await _userAccessModule.ExecuteQueryAsync(new GetUsersQuery(input.Search, input.Pagination));

                result.Add(Result<PaginationDto<User>>.Resolve(
                    new PaginationDto<User>(dto.Items.Select(u => new User(u)), dto.Total)));
            }

            return result;
        }
    }
}