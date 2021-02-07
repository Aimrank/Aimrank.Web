import { useNotifications } from "@/modules/common/hooks/useNotifications";
import { Hub } from "@/modules/common/hubs/Hub";
import { MatchStatus } from "@/modules/match/services/MatchService";
import { useLobby } from "../hooks/useLobby";
import {
  IInvitationAcceptedEvent,
  // IInvitationCanceledEvent,
  // IInvitationCreatedEvent,
  ILobbyConfigurationChangedEvent,
  ILobbyStatusChangedEvent,
  IMatchFinishedEvent,
  IMatchStartingEvent,
  IMatchStartedEvent,
  IMemberLeftEvent,
  IMemberRoleChangedEvent
} from "./LobbyHubEvents";

export class LobbyHub {
  private readonly lobby = useLobby();
  private readonly notifications = useNotifications();

  constructor(private readonly hub: Hub) {
    hub.connection.on("Disconnect", this.onDisconnect.bind(this));
    hub.connection.on("InvitationAccepted", this.onInvitationAccepted.bind(this));
    // hub.connection.on("InvitationCanceled", this.onInvitationCanceled.bind(this));
    // hub.connection.on("InvitationCreated", this.onInvitationCreated.bind(this));
    hub.connection.on("LobbyConfigurationChanged", this.onLobbyConfigurationChanged.bind(this));
    hub.connection.on("LobbyStatusChanged", this.onLobbyStatusChanged.bind(this));
    hub.connection.on("MatchFinished", this.onMatchFinished.bind(this));
    hub.connection.on("MatchStarting", this.onMatchStarting.bind(this));
    hub.connection.on("MatchStarted", this.onMatchStarted.bind(this));
    hub.connection.on("MemberLeft", this.onMemberLeft.bind(this));
    hub.connection.on("MemberRoleChanged", this.onMemberRoleChanged.bind(this));
  }

  public async connect() {
    await this.hub.connect();
  }

  public async disconnect() {
    await this.hub.disconnect();
  }

  private async onDisconnect() {
    await this.hub.disconnect();
  }

  private onInvitationAccepted(event: IInvitationAcceptedEvent) {
    this.lobby.addMember({ userId: event.invitedUserId, isLeader: false });
  }

  // private onInvitationCanceled(event: IInvitationCanceledEvent) {}
  // private onInvitationCreated(event: IInvitationCreatedEvent) {}

  private onLobbyConfigurationChanged(event: ILobbyConfigurationChangedEvent) {
    this.lobby.setLobbyConfiguration({
      map: event.map,
      name: event.name,
      mode: event.mode
    });
  }

  private onLobbyStatusChanged(event: ILobbyStatusChangedEvent) {
    this.lobby.setLobbyStatus(event.status);
  }

  private onMatchStarting(event: IMatchStartingEvent) {
    this.lobby.setMatch({
      id: event.matchId,
      map: event.map,
      status: MatchStatus.Starting,
      address: event.address
    });

    this.notifications.success("Searching for new game...");
  }

  private onMatchStarted(event: IMatchStartedEvent) {
    this.lobby.setMatch({
      id: event.matchId,
      map: event.map,
      status: MatchStatus.Started,
      address: event.address
    });

    this.notifications.success(`Match created: ${event.address}`);
  }

  private onMatchFinished(event: IMatchFinishedEvent) {
    this.lobby.clearLobby();
    this.lobby.clearMatch();

    this.notifications.success("Match finished.");
  }

  private onMemberLeft(event: IMemberLeftEvent) {
    this.lobby.removeMember(event.userId);
  }

  private onMemberRoleChanged(event: IMemberRoleChangedEvent) {
    this.lobby.changeMemberRole(event.userId, event.role);
  }
}