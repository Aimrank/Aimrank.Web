using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.App.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Friendships.GetBlockedUsers;

namespace Aimrank.Web.App.GraphQL.Queries.DataLoaders
{
    public class BlockedUsersDataLoader : DataLoaderBase<PaginationQuery, PaginationDto<User>>
    {
        private readonly IUserAccessModule _userAccessModule;
        
        public BlockedUsersDataLoader(IBatchScheduler batchScheduler, IUserAccessModule userAccessModule)
            : base(batchScheduler)
        {
            _userAccessModule = userAccessModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<PaginationDto<User>>>> FetchAsync(IReadOnlyList<PaginationQuery> keys, CancellationToken cancellationToken)
        {
            var result = new List<Result<PaginationDto<User>>>();

            foreach (var pagination in keys)
            {
                var dto = await _userAccessModule.ExecuteQueryAsync(new GetBlockedUsersQuery(pagination));
                
                result.Add(Result<PaginationDto<User>>.Resolve(
                    new PaginationDto<User>(dto.Items.Select(u => new User(u)), dto.Total)));
            }

            return result;
        }
    }
}