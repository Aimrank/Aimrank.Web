import { userService } from "~/services";
import { Hub } from "@/common/hubs/Hub";
import { MatchStatus } from "@/profile/models/MatchStatus";
import { LobbyStatus } from "@/lobby/models/ILobbyStatus";
import { useUser } from "@/profile/hooks/useUser";
import { useMatch } from "@/lobby/hooks/useMatch";
import { useLobby } from "@/lobby/hooks/useLobby";
import { useNotifications } from "@/common/hooks/useNotifications";
import {
  IInvitationAcceptedEvent,
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
  IMatchCanceledEvent,
  IMatchPlayerLeftEvent,
} from "./LobbyHubEvents";

export class LobbyHub {
  private readonly user = useUser();
  private readonly lobby = useLobby();
  private readonly match = useMatch();
  private readonly notifications = useNotifications();

  constructor(private readonly hub: Hub) {
    hub.connection.on("Disconnect", this.onDisconnect.bind(this));
    hub.connection.on("InvitationAccepted", this.onInvitationAccepted.bind(this));
    hub.connection.on("LobbyConfigurationChanged", this.onLobbyConfigurationChanged.bind(this));
    hub.connection.on("LobbyStatusChanged", this.onLobbyStatusChanged.bind(this));
    hub.connection.on("MatchReady", this.onMatchReady.bind(this));
    hub.connection.on("MatchAccepted", this.onMatchAccepted.bind(this));
    hub.connection.on("MatchTimedOut", this.onMatchTimedOut.bind(this));
    hub.connection.on("MatchStarting", this.onMatchStarting.bind(this));
    hub.connection.on("MatchStarted", this.onMatchStarted.bind(this));
    hub.connection.on("MatchCanceled", this.onMatchCanceled.bind(this));
    hub.connection.on("MatchFinished", this.onMatchFinished.bind(this));
    hub.connection.on("MatchPlayerLeft", this.onMatchPlayerLeft.bind(this));
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
        userId: result.value.id,
        username: result.value.username,
        isLeader: false
      });
    }
  }

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

  private onMatchCanceled(event: IMatchCanceledEvent) {
    this.match.clearMatch();
    this.lobby.setLobbyStatus(LobbyStatus.Open);

    this.notifications.warning("Some players failed to connect. Match is canceled.");
  }

  private onMatchFinished(event: IMatchFinishedEvent) {
    this.match.clearMatch();
    this.lobby.setLobbyStatus(LobbyStatus.Open);

    this.notifications.success("Match finished.");
  }

  private onMatchPlayerLeft(event: IMatchPlayerLeftEvent) {
    this.notifications.warning("Failed to reconnect. You will be penalized for leaving early when the match is over.");
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