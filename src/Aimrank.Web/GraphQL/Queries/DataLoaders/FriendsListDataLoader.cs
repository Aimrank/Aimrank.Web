using Aimrank.Common.Application.Queries;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Friendships.GetFriendsList;
using GreenDonut;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.DataLoaders
{
    public record FriendsListDataLoaderInput
    {
        public Guid Id { get; }
        public PaginationQuery Pagination { get; }

        public FriendsListDataLoaderInput(Guid id, PaginationQuery pagination)
        {
            Id = id;
            Pagination = pagination;
        }
    }
    
    public class FriendsListDataLoader : DataLoaderBase<FriendsListDataLoaderInput, PaginationDto<Guid>>
    {
        private readonly IUserAccessModule _userAccessModule;
        
        public FriendsListDataLoader(IBatchScheduler batchScheduler, IUserAccessModule userAccessModule)
            : base(batchScheduler)
        {
            _userAccessModule = userAccessModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<PaginationDto<Guid>>>> FetchAsync(
            IReadOnlyList<FriendsListDataLoaderInput> keys, CancellationToken cancellationToken)
        {
            var result = new List<Result<PaginationDto<Guid>>>();

            foreach (var input in keys)
            {
                var users = await _userAccessModule.ExecuteQueryAsync(new GetFriendsListQuery(input.Id, input.Pagination));
                
                result.Add(Result<PaginationDto<Guid>>.Resolve(users));
            }

            return result;
        }
    }
}