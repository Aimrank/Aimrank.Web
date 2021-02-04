import { HubConnection, HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";
import { httpClient } from "@/services";

export class GeneralHub {
  public readonly connection: HubConnection;

  constructor(endpoint: string) {
    this.connection = new HubConnectionBuilder()
      .withUrl(endpoint, {
        accessTokenFactory: httpClient.accessTokenFactory.bind(httpClient)
      })
      .build();
  }

  public async connect() {
    if (this.connection.state === HubConnectionState.Disconnected) {
      return this.connection.start();
    }
  }

  public async disconnect() {
    const states = [
      HubConnectionState.Connected,
      HubConnectionState.Connecting,
      HubConnectionState.Reconnecting
    ];

    if (states.some(s => s === this.connection.state)) {
      return this.connection.stop();
    }
  }
}
