using Aimrank.Common.Application.Events;
using System.Collections.Generic;
using System;

namespace Aimrank.IntegrationEvents
{
    public class ServerCreatedEvent : IntegrationEvent
    {
        public Guid ServerId { get; }
        public string Address { get; }
        public string Map { get; }
        public IEnumerable<Guid> Players { get; }

        public ServerCreatedEvent(
            Guid id,
            Guid serverId,
            string address,
            string map,
            IEnumerable<Guid> players,
            DateTime occurredAt)
            : base(id, occurredAt)
        {
            ServerId = serverId;
            Address = address;
            Map = map;
            Players = players;
        }
    }
}