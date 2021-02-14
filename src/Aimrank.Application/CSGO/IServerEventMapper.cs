using System;

namespace Aimrank.Application.CSGO
{
    public interface IServerEventMapper
    {
        IServerEventCommand Map(Guid matchId, string name, dynamic data);
    }
}