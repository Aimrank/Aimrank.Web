using Aimrank.Web.GraphQL.Queries;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate;
using System;

namespace Aimrank.Web.GraphQL.Mutations
{
    public record MutationPayloadBase
    {
        public Query GetQuery(IResolverContext context) => context.GetQueryRoot<Query>();
    }
    
    // Users

    public record AuthenticationSuccessRecord(
        [GraphQLNonNullType] Guid Id,
        [GraphQLNonNullType] string Username,
        [GraphQLNonNullType] string Email);
    
    public record SignInPayload(AuthenticationSuccessRecord Record) : MutationPayloadBase;
    public record SignUpPayload(AuthenticationSuccessRecord Record) : MutationPayloadBase;
    
    public record SignOutPayload(bool Record = true) : MutationPayloadBase;
    
    
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