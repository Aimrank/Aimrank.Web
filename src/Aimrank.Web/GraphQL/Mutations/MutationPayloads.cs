using Aimrank.Web.GraphQL.Queries;
using HotChocolate.Resolvers;

namespace Aimrank.Web.GraphQL.Mutations
{
    public record MutationPayloadBase
    {
        public Query GetQuery(IResolverContext context) => context.GetQueryRoot<Query>();
    }
    
    // Users

    public record SignInPayload : MutationPayloadBase;
    public record SignUpPayload : MutationPayloadBase;
    public record SignOutPayload : MutationPayloadBase;
    
    // Friends

    public record InviteUserToFriendsListPayload : MutationPayloadBase;
    public record AcceptFriendshipInvitationPayload : MutationPayloadBase;
    public record DeclineFriendshipInvitationPayload : MutationPayloadBase;
    public record BlockUserPayload : MutationPayloadBase;
    public record UnblockUserPayload : MutationPayloadBase;
    public record DeleteFriendshipPayload : MutationPayloadBase;
    
    // Matches

    public record AcceptMatchPayload : MutationPayloadBase;
    
    // Lobbies

    public record CreateLobbyPayload : MutationPayloadBase;
    public record InviteUserToLobbyPayload : MutationPayloadBase;
    public record AcceptLobbyInvitationPayload : MutationPayloadBase;
    public record CancelLobbyInvitationPayload : MutationPayloadBase;
    public record ChangeLobbyConfigurationPayload : MutationPayloadBase;
    public record LeaveLobbyPayload : MutationPayloadBase;
    public record StartSearchingForGamePayload : MutationPayloadBase;
    public record CancelSearchingForGamePayload : MutationPayloadBase;
}