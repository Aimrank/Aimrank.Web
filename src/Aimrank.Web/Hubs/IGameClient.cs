using System.Threading.Tasks;

namespace Aimrank.Web.Hubs
{
    public interface IGameClient
    {
        Task EventReceived(string content);
    }
}