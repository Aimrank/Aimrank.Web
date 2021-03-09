using Aimrank.Web.Hubs.General.Messages;
using System.Threading.Tasks;

namespace Aimrank.Web.Hubs.General
{
    public interface IGeneralClient
    {
        Task LobbyInvitationCreated(LobbyInvitationCreatedEventMessage eventMessage);
        Task FriendshipInvitationCreated(FriendshipInvitationCreatedEventMessage eventMessage);
    }
}