using Aimrank.Common.Application.Queries;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Friendships.GetFriendshipInvitations;
using Aimrank.Web.GraphQL.Queries.Models;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public class FriendshipInvitationsDataLoader : DataLoaderBase<PaginationQuery, PaginationDto<User>>
    {
        private readonly IUserAccessModule _userAccessModule;
        
        public FriendshipInvitationsDataLoader(IBatchScheduler batchScheduler, IUserAccessModule userAccessModule)
            : base(batchScheduler)
        {
            _userAccessModule = userAccessModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<PaginationDto<User>>>> FetchAsync(IReadOnlyList<PaginationQuery> keys, CancellationToken cancellationToken)
        {
            var result = new List<Result<PaginationDto<User>>>();

            foreach (var pagination in keys)
            {
                var dto = await _userAccessModule.ExecuteQueryAsync(new GetFriendshipInvitationsQuery(pagination));

                result.Add(Result<PaginationDto<User>>.Resolve(
                    new PaginationDto<User>(dto.Items.Select(u => new User(u)), dto.Total)));
            }

            return result;
        }
    }
}