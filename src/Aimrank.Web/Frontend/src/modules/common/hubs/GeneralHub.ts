import { Hub } from "./Hub";

interface IInvitationCreatedEvent {
  lobbyId: string;
  invitingUserId: string;
  invitedUserId: string;
}

export class GeneralHub {
  constructor(private readonly hub: Hub) {
    hub.connection.on("InvitationCreated", this.onInvitationCreated.bind(this));
  }

  public async connect() {
    await this.hub.connect();
  }

  public async disconnect() {
    await this.hub.disconnect();
  }

  private onInvitationCreated(event: IInvitationCreatedEvent) {
    console.log("[General] InvitationCreated", event);
  }
}
