namespace Aimrank.Web.App.GraphQL.Mutations.Friendships
{
    public record InviteUserToFriendsListPayload : MutationPayloadBase;
    public record AcceptFriendshipInvitationPayload : MutationPayloadBase;
    public record DeclineFriendshipInvitationPayload : MutationPayloadBase;
    public record BlockUserPayload : MutationPayloadBase;
    public record UnblockUserPayload : MutationPayloadBase;
    public record DeleteFriendshipPayload : MutationPayloadBase;
}