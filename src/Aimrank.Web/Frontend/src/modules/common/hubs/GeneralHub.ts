import { lobbyService } from "~/services";
import { useNotifications } from "@/common/hooks/useNotifications";
import { Hub } from "./Hub";
import {
  IFriendshipInvitationCreatedEvent,
  ILobbyInvitationCreatedEvent
} from "./GeneralHubEvents";

interface IInvitationCreatedEvent {
  lobbyId: string;
  invitingUserId: string;
  invitedUserId: string;
}

export class GeneralHub {
  private readonly notifications = useNotifications();

  constructor(private readonly hub: Hub) {
    hub.connection.on("FriendshipInvitationCreated", this.onFriendshipInvitationCreated.bind(this));
  }

  public async connect() {
    await this.hub.connect();
  }

  public async disconnect() {
    await this.hub.disconnect();
  }

  // private async onLobbyInvitationCreated(event: ILobbyInvitationCreatedEvent) {
  //   const result = await lobbyService.getInvitations();

  //   if (result.isOk()) {
  //     this.invitations.setInvitations(result.value);
  //   }

  //   this.notifications.success("You have been invited to lobby");
  // }

  private async onFriendshipInvitationCreated(event: IFriendshipInvitationCreatedEvent) {
    this.notifications.success("You have received friendship invitation.");
  }
}
