import { Hub } from "@/modules/common/hubs/Hub";
import {
  IInvitationAcceptedEvent,
  IInvitationCanceledEvent,
  IInvitationCreatedEvent,
  ILobbyConfigurationChangedEvent,
  ILobbyStatusChangedEvent,
  IMatchFinishedEvent,
  IMatchStartingEvent,
  IMatchStartedEvent,
  IMemberLeftEvent,
  IMemberRoleChangedEvent
} from "./LobbyHubEvents";

export class LobbyHub {
  constructor(private readonly hub: Hub) {
    hub.connection.on("Disconnect", this.onDisconnect.bind(this));
    hub.connection.on("InvitationAccepted", this.onInvitationAccepted.bind(this));
    hub.connection.on("InvitationCanceled", this.onInvitationCanceled.bind(this));
    hub.connection.on("InvitationCreated", this.onInvitationCreated.bind(this));
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
    console.log("InvitationAccepted", event);
  }

  private onInvitationCanceled(event: IInvitationCanceledEvent) {
    console.log("InvitationCanceled", event);
  }

  private onInvitationCreated(event: IInvitationCreatedEvent) {
    console.log("InvitationCreated", event);
  }

  private onLobbyConfigurationChanged(event: ILobbyConfigurationChangedEvent) {
    console.log("LobbyConfigurationChanged", event)
  }

  private onLobbyStatusChanged(event: ILobbyStatusChangedEvent) {
    console.log("LobbyStatusChanged", event);
  }

  private onMatchStarting(event: IMatchStartingEvent) {
    console.log("MatchStarting", event);
  }

  private onMatchStarted(event: IMatchStartedEvent) {
    console.log("MatchStarted", event);
  }

  private onMatchFinished(event: IMatchFinishedEvent) {
    console.log("MatchFinished", event);
  }

  private onMemberLeft(event: IMemberLeftEvent) {
    console.log("MemberLeft", event);
  }

  private onMemberRoleChanged(event: IMemberRoleChangedEvent) {
    console.log("MemberRoleChanged", event);
  }
}