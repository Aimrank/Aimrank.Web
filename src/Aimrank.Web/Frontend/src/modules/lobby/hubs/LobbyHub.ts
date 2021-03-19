import { Hub } from "@/common/hubs/Hub";
import {
  IMatchFinishedEvent,
  IMatchStartingEvent,
  IMatchStartedEvent,
  IMatchReadyEvent,
  IMatchAcceptedEvent,
  IMatchTimedOutEvent,
  IMatchCanceledEvent,
  IMatchPlayerLeftEvent,
} from "./LobbyHubEvents";

export class LobbyHub {
  constructor(private readonly hub: Hub) {
    hub.connection.on("Disconnect", this.onDisconnect.bind(this));
    hub.connection.on("MatchReady", this.onMatchReady.bind(this));
    hub.connection.on("MatchAccepted", this.onMatchAccepted.bind(this));
    hub.connection.on("MatchTimedOut", this.onMatchTimedOut.bind(this));
    hub.connection.on("MatchStarting", this.onMatchStarting.bind(this));
    hub.connection.on("MatchStarted", this.onMatchStarted.bind(this));
    hub.connection.on("MatchCanceled", this.onMatchCanceled.bind(this));
    hub.connection.on("MatchFinished", this.onMatchFinished.bind(this));
    hub.connection.on("MatchPlayerLeft", this.onMatchPlayerLeft.bind(this));
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

  private async onMatchReady(event: IMatchReadyEvent) {
    // this.match.setMatch({
    //   id: event.matchId,
    //   map: event.map,
    //   mode: 0,
    //   status: MatchStatus.Ready,
    //   address: "",
    //   expiresAt: event.expiresAt
    // });
  }

  private onMatchAccepted(event: IMatchAcceptedEvent) {
    // this.match.addAcceptation(event.userId);
  }

  private onMatchTimedOut(event: IMatchTimedOutEvent) {
    // this.match.clearMatch();

    // this.notifications.danger("Some users failed to accept game.");
  }

  private onMatchStarting(event: IMatchStartingEvent) {
    // this.match.setMatchStatus(MatchStatus.Starting);

    // this.notifications.success("Starting server...");
  }

  private onMatchStarted(event: IMatchStartedEvent) {
    // this.match.setMatch({
    //   id: event.matchId,
    //   map: event.map,
    //   mode: event.mode,
    //   status: MatchStatus.Started,
    //   address: event.address
    // });

    // this.notifications.success(`Match created: aimrank.pl${event.address.slice(event.address.indexOf(":"))}`);
  }

  private onMatchCanceled(event: IMatchCanceledEvent) {
    // this.match.clearMatch();
    // this.lobby.setLobbyStatus(LobbyStatus.Open);

    // this.notifications.warning("Some players failed to connect. Match is canceled.");
  }

  private onMatchFinished(event: IMatchFinishedEvent) {
    // this.match.clearMatch();
    // this.lobby.setLobbyStatus(LobbyStatus.Open);

    // this.notifications.success("Match finished.");
  }

  private onMatchPlayerLeft(event: IMatchPlayerLeftEvent) {
    // this.notifications.warning("Failed to reconnect. You will be penalized for leaving early when the match is over.");
  }
}