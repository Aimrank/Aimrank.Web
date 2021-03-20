using Aimrank.Web.GraphQL.Queries.Models;
using HotChocolate.Types.Pagination;
using HotChocolate.Types;

namespace Aimrank.Web.GraphQL.Queries
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor
                .Field(f => f.GetUsers(default, default, default, default))
                .Type<ConnectionCountType<NonNullType<UserType>>>();
            
            descriptor
                .Field(f => f.GetMatches(default, default, default, default))
                .Type<ConnectionCountType<NonNullType<MatchType>>>();

            descriptor
                .Field(f => f.GetBlockedUsers(default, default, default))
                .Type<ConnectionCountType<NonNullType<UserType>>>();
            
            descriptor
                .Field(f => f.GetFriendshipInvitations(default, default, default))
                .Type<ConnectionCountType<NonNullType<UserType>>>();
            
            descriptor
                .Field(f => f.GetLobbyInvitations(default))
                .Type<ListType<NonNullType<LobbyInvitationType>>>();
        }
    }
}