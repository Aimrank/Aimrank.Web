import { useNotifications } from "@/modules/common/hooks/useNotifications";
import { Hub } from "@/modules/common/hubs/Hub";
import { MatchStatus } from "@/modules/match/services/MatchService";
import { LobbyStatus } from "../services/LobbyService";
import { useUser } from "@/modules/user";
import { useMatch } from "@/modules/match/hooks/useMatch";
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
  IMemberRoleChangedEvent,
  IMatchReadyEvent,
  IMatchAcceptedEvent,
  IMatchTimedOutEvent,
} from "./LobbyHubEvents";
import { matchService, userService } from "@/services";

export class LobbyHub {
  private readonly user = useUser();
  private readonly lobby = useLobby();
  private readonly match = useMatch();
  private readonly notifications = useNotifications();

  constructor(private readonly hub: Hub) {
    hub.connection.on("Disconnect", this.onDisconnect.bind(this));
    hub.connection.on("InvitationAccepted", this.onInvitationAccepted.bind(this));
    // hub.connection.on("InvitationCanceled", this.onInvitationCanceled.bind(this));
    // hub.connection.on("InvitationCreated", this.onInvitationCreated.bind(this));
    hub.connection.on("LobbyConfigurationChanged", this.onLobbyConfigurationChanged.bind(this));
    hub.connection.on("LobbyStatusChanged", this.onLobbyStatusChanged.bind(this));
    hub.connection.on("MatchReady", this.onMatchReady.bind(this));
    hub.connection.on("MatchAccepted", this.onMatchAccepted.bind(this));
    hub.connection.on("MatchTimedOut", this.onMatchTimedOut.bind(this));
    hub.connection.on("MatchStarting", this.onMatchStarting.bind(this));
    hub.connection.on("MatchStarted", this.onMatchStarted.bind(this));
    hub.connection.on("MatchFinished", this.onMatchFinished.bind(this));
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

  private async onInvitationAccepted(event: IInvitationAcceptedEvent) {
    const result = await userService.getUserDetails(event.invitedUserId);

    if (result.isOk()) {
      this.lobby.addMember({
        userId: result.value.userId,
        username: result.value.username,
        isLeader: false
      });
    }
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

  private async onMatchReady(event: IMatchReadyEvent) {
    this.match.setMatch({
      id: event.matchId,
      map: event.map,
      mode: 0,
      status: MatchStatus.Ready,
      address: "",
      expiresAt: event.expiresAt
    });
  }

  private onMatchAccepted(event: IMatchAcceptedEvent) {
    this.match.addAcceptation(event.userId);
  }

  private onMatchTimedOut(event: IMatchTimedOutEvent) {
    this.match.clearMatch();

    this.notifications.danger("Some users failed to accept game.");
  }

  private onMatchStarting(event: IMatchStartingEvent) {
    this.match.setMatchStatus(MatchStatus.Starting);

    this.notifications.success("Starting server...");
  }

  private onMatchStarted(event: IMatchStartedEvent) {
    this.match.setMatch({
      id: event.matchId,
      map: event.map,
      mode: event.mode,
      status: MatchStatus.Started,
      address: event.address
    });

    this.notifications.success(`Match created: aimrank.pl${event.address.slice(event.address.indexOf(":"))}`);
  }

  private onMatchFinished(event: IMatchFinishedEvent) {
    this.match.clearMatch();
    this.lobby.setLobbyStatus(LobbyStatus.Open);

    this.notifications.success("Match finished.");
  }

  private onMemberLeft(event: IMemberLeftEvent) {
    this.lobby.removeMember(event.userId);
  }

  private onMemberRoleChanged(event: IMemberRoleChangedEvent) {
    this.lobby.changeMemberRole(event.userId, event.role);

    if (this.user.state.user?.id === event.userId && event.role === 1) {
      this.notifications.success("You are now lobby leader");
    }
  }
}