using Aimrank.Web.App.GraphQL.Queries.DataLoaders;
using Aimrank.Web.App.GraphQL.Queries.Models;
using HotChocolate;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.App.GraphQL.Mutations.Lobbies
{
    public record CreateLobbyPayload : MutationPayloadBase
    {
        public Guid RecordId { get; }

        public CreateLobbyPayload(Guid recordId)
        {
            RecordId = recordId;
        }

        public Task<Lobby> GetRecord([DataLoader] LobbyDataLoader loader)
            => loader.LoadAsync(0, CancellationToken.None);
    }
    
    public record InvitePlayerToLobbyPayload : MutationPayloadBase;
    public record KickPlayerFromLobbyPayload : MutationPayloadBase;
    public record AcceptLobbyInvitationPayload : MutationPayloadBase;
    public record CancelLobbyInvitationPayload : MutationPayloadBase;
    public record ChangeLobbyConfigurationPayload : MutationPayloadBase;
    public record LeaveLobbyPayload : MutationPayloadBase;
    public record StartSearchingForGamePayload : MutationPayloadBase;
    public record CancelSearchingForGamePayload : MutationPayloadBase;
    public record AcceptMatchPayload : MutationPayloadBase;
}