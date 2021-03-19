using Aimrank.Common.Application.Queries;
using Aimrank.Modules.Matches.Application.Players.GetPlayerStatsBatch;
using Aimrank.Modules.UserAccess.Application.Users.GetUserBatch;
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
        public string Username { get; }

        public User(UserDto dto)
        {
            Id = dto.Id;
            Username = dto.Username;
        }

        public Task<PlayerStatsDto> GetStats([DataLoader] PlayerStatsDataLoader loader)
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

        public Task<string> GetSteamId([DataLoader] SteamIdDataLoader loader)
            => loader.LoadAsync(Id, CancellationToken.None);
    }

    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor
                .Field(f => f.GetFriends(default, default, default, default))
                .Type<ConnectionCountType<UserType>>();

            descriptor
                .Field(f => f.Username)
                .Type<NonNullType<StringType>>();
        }
    }
}