using Aimrank.Web.Hubs.Lobbies.Messages;
using System.Threading.Tasks;

namespace Aimrank.Web.Hubs.General
{
    public interface IGeneralClient
    {
        Task InvitationCreated(InvitationCreatedEventMessage eventMessage);
    }
}