using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Queries.GetServerProcesses
{
    public class GetServerProcessQuery : IQuery<ServerProcessDto>
    {
        public Guid ServerId { get; }

        public GetServerProcessQuery(Guid serverId)
        {
            ServerId = serverId;
        }
    }
}