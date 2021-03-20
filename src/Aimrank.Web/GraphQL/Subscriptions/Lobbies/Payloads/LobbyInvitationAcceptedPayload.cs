using Aimrank.Web.GraphQL.Queries.DataLoaders;
using Aimrank.Web.GraphQL.Queries.Models;
using HotChocolate;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record LobbyInvitationAcceptedPayload(
        [GraphQLNonNullType] LobbyInvitationAcceptedRecord Record) : SubscriptionPayloadBase;
    
    public class LobbyInvitationAcceptedRecord
    {
        private readonly Guid _invitedPlayerId;
        
        public Guid LobbyId { get; }

        public LobbyInvitationAcceptedRecord(Guid lobbyId, Guid invitedPlayerId)
        {
            LobbyId = lobbyId;
            _invitedPlayerId = invitedPlayerId;
        }

        public Task<User> GetInvitingUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_invitedPlayerId, CancellationToken.None);
    }
}