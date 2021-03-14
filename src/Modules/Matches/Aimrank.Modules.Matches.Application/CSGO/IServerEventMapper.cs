using System;

namespace Aimrank.Modules.Matches.Application.CSGO
{
    public interface IServerEventMapper
    {
        IServerEventCommand Map(Guid matchId, string name, dynamic data);
    }
}