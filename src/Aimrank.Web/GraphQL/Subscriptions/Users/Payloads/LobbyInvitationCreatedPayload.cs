using Aimrank.Web.GraphQL.Queries.DataLoaders;
using Aimrank.Web.GraphQL.Queries.Models;
using HotChocolate;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Users.Payloads
{
    public record LobbyInvitationCreatedPayload(
        [GraphQLNonNullType] LobbyInvitationCreatedRecord Record) : SubscriptionPayloadBase;

    public class LobbyInvitationCreatedRecord
    {
        private readonly Guid _invitingPlayerId;
        public Guid LobbyId { get; }

        public LobbyInvitationCreatedRecord(Guid lobbyId, Guid invitingPlayerId)
        {
            LobbyId = lobbyId;
            _invitingPlayerId = invitingPlayerId;
        }

        public Task<User> GetInvitingUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_invitingPlayerId, CancellationToken.None);
    }
}