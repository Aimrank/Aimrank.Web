using HotChocolate.Types;
using HotChocolate;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Lobbies.Payloads
{
    public record LobbyConfigurationChangedPayload(
        [GraphQLNonNullType] LobbyConfigurationChangedRecord Record) : SubscriptionPayloadBase;

    public record LobbyConfigurationChangedRecord(Guid LobbyId, int Mode, string Name, IEnumerable<string> Maps);

    public class LobbyConfigurationChangedRecordType : ObjectType<LobbyConfigurationChangedRecord>
    {
        protected override void Configure(IObjectTypeDescriptor<LobbyConfigurationChangedRecord> descriptor)
        {
            descriptor.Field(f => f.Name)
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Maps)
                .Type<NonNullType<ListType<NonNullType<StringType>>>>();
        }
    }
}