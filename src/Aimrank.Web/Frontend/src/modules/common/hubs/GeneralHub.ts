import { lobbyService } from "~/services";
import { useInvitations } from "@/lobby/hooks/useInvitations";
import { useNotifications } from "@/common/hooks/useNotifications";
import { Hub } from "./Hub";

interface IInvitationCreatedEvent {
  lobbyId: string;
  invitingUserId: string;
  invitedUserId: string;
}

export class GeneralHub {
  private readonly notifications = useNotifications();
  private readonly invitations = useInvitations();

  constructor(private readonly hub: Hub) {
    hub.connection.on("InvitationCreated", this.onInvitationCreated.bind(this));
  }

  public async connect() {
    await this.hub.connect();
  }

  public async disconnect() {
    await this.hub.disconnect();
  }

  private async onInvitationCreated(event: IInvitationCreatedEvent) {
    const result = await lobbyService.getInvitations();

    if (result.isOk()) {
      this.invitations.setInvitations(result.value);
    }

    this.notifications.success("You have been invited to lobby");
  }
}
