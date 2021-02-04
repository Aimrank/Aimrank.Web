using System;

namespace Aimrank.Application.CSGO
{
    public interface IServerEventMapper
    {
        IServerEventCommand Map(Guid serverId, string name, dynamic data);
    }
}