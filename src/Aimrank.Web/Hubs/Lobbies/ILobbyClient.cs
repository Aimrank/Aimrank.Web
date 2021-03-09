using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.IntegrationEvents.Matches;
using Aimrank.Web.Hubs.General.Messages;
using Aimrank.Web.Hubs.Lobbies.Messages;
using System.Threading.Tasks;

namespace Aimrank.Web.Hubs.Lobbies
{
    public interface ILobbyClient
    {
        Task Disconnect();
        Task MatchAccepted(MatchAcceptedEvent @event);
        Task MatchReady(MatchReadyEventMessage @event);
        Task MatchStarting(MatchStartingEvent @event);
        Task MatchTimedOut(MatchTimedOutEvent @event);
        Task MatchCanceled(MatchCanceledEvent @event);
        Task MatchStarted(MatchStartedEvent @event);
        Task MatchFinished(MatchFinishedEvent @event);
        Task MatchPlayerLeft(MatchPlayerLeftEvent @event);
        Task InvitationAccepted(InvitationAcceptedEventMessage eventMessage);
        Task InvitationCanceled(InvitationCanceledEventMessage eventMessage);
        Task InvitationCreated(LobbyInvitationCreatedEventMessage eventMessage);
        Task LobbyConfigurationChanged(LobbyConfigurationChangedEventMessage @event);
        Task LobbyStatusChanged(LobbyStatusChangedEvent @event);
        Task MemberLeft(MemberLeftEvent @event);
        Task MemberRoleChanged(MemberRoleChangedEvent @event);
    }
}