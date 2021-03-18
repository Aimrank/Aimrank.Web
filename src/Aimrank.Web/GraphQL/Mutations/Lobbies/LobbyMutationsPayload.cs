namespace Aimrank.Web.GraphQL.Mutations.Lobbies
{
    public record CreateLobbyPayload : MutationPayloadBase;
    public record InviteUserToLobbyPayload : MutationPayloadBase;
    public record AcceptLobbyInvitationPayload : MutationPayloadBase;
    public record CancelLobbyInvitationPayload : MutationPayloadBase;
    public record ChangeLobbyConfigurationPayload : MutationPayloadBase;
    public record LeaveLobbyPayload : MutationPayloadBase;
    public record StartSearchingForGamePayload : MutationPayloadBase;
    public record CancelSearchingForGamePayload : MutationPayloadBase;
    public record AcceptMatchPayload : MutationPayloadBase;
}