using System;

namespace Aimrank.Application.Queries.GetServerProcesses
{
    public class ServerProcessDto
    {
        public Guid ServerId { get; init; }
        public int Port { get; init; }
    }
}