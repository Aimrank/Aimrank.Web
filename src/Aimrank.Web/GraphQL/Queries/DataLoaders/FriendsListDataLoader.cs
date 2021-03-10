using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Friendships.GetFriendsList;
using Aimrank.Common.Application.Queries;
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
        private readonly IAimrankModule _aimrankModule;
        
        public FriendsListDataLoader(IBatchScheduler batchScheduler, IAimrankModule aimrankModule)
            : base(batchScheduler)
        {
            _aimrankModule = aimrankModule;
        }

        protected override async ValueTask<IReadOnlyList<Result<PaginationDto<Guid>>>> FetchAsync(
            IReadOnlyList<FriendsListDataLoaderInput> keys, CancellationToken cancellationToken)
        {
            var result = new List<Result<PaginationDto<Guid>>>();

            foreach (var input in keys)
            {
                var users = await _aimrankModule.ExecuteQueryAsync(new GetFriendsListQuery(input.Id, input.Pagination));
                
                result.Add(Result<PaginationDto<Guid>>.Resolve(users));
            }

            return result;
        }
    }
}