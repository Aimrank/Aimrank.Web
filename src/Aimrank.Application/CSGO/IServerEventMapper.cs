namespace Aimrank.Application.CSGO
{
    public interface IServerEventMapper
    {
        IServerEventCommand Map(string content);
    }
}