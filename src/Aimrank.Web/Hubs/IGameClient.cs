using System.Threading.Tasks;

namespace Aimrank.Web.Hubs
{
    public interface IGameClient
    {
        Task MessageReceived(string message);
        Task ServerMessageReceived(string message);
    }
}