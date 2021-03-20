using Aimrank.Web.GraphQL.Queries.DataLoaders;
using Aimrank.Web.GraphQL.Queries.Models;
using HotChocolate;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Users.Payloads
{
    public record FriendshipInvitationCreatedPayload(
        [GraphQLNonNullType] FriendshipInvitationCreatedRecord Record) : SubscriptionPayloadBase;

    public class FriendshipInvitationCreatedRecord
    {
        private readonly Guid _invitingUserId;

        public FriendshipInvitationCreatedRecord(Guid invitingUserId)
        {
            _invitingUserId = invitingUserId;
        }

        public Task<User> GetInvitingUser([DataLoader] UserDataLoader loader)
            => loader.LoadAsync(_invitingUserId, CancellationToken.None);
    }
}