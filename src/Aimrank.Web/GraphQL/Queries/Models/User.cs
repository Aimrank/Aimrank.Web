using Aimrank.Application.Queries.Users.GetUserBatch;
using Aimrank.Application.Queries.Users.GetUserStatsBatch;
using Aimrank.Common.Application.Queries;
using Aimrank.Web.GraphQL.Queries.DataLoaders;
using HotChocolate.Types.Pagination;
using HotChocolate.Types;
using HotChocolate;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Queries.Models
{
    public class User
    {
        public Guid Id { get; }
        public string SteamId { get; }
        public string Username { get; }

        public User(UserDto dto)
        {
            Id = dto.Id;
            SteamId = dto.SteamId;
            Username = dto.Username;
        }

        public Task<UserStatsDto> GetStats([DataLoader] UserStatsDataLoader loader)
            => loader.LoadAsync(Id, CancellationToken.None);
        
        public async Task<Connection<User>> GetFriends(
            int? skip,
            int? take,
            [DataLoader] UserDataLoader userDataLoader,
            [DataLoader] FriendsListDataLoader friendsListDataLoader)
        {
            var input = new FriendsListDataLoaderInput(Id, new PaginationQuery(skip, take));
            var result = await friendsListDataLoader.LoadAsync(input, CancellationToken.None);

            return new PaginationDto<User>(
                await userDataLoader.LoadAsync(result.Items.ToList(), CancellationToken.None),
                result.Total)
                .AsConnection(skip, take);
        }
    }

    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor
                .Field(f => f.GetFriends(default, default, default, default))
                .Type<ConnectionCountType<UserType>>();
        }
    }
}