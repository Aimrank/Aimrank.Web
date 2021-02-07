import { useNotifications } from "../hooks/useNotifications";
import { Hub } from "./Hub";

interface IInvitationCreatedEvent {
  lobbyId: string;
  invitingUserId: string;
  invitedUserId: string;
}

export class GeneralHub {
  private readonly notifications = useNotifications();

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
    this.notifications.success("You have been invited to lobby");
  }
}
