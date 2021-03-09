import { HttpClient } from "@/common/services/HttpClient";
import { Service } from "@/common/services/Service";
import { IUserDto } from "@/profile/models/IUserDto";
import { IFriendshipDto } from "@/profile/models/IFriendshipDto";

export class FriendshipService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      getFriendship: "/friendship/{userId1}/{userId2}",
      getFriendsList: "/friendship/{userId}",
      getFriendshipInvitations: "/friendship/invitations",
      getBlockedUsers: "/friendship/blocked",
      invite: "/friendship/invite",
      accept: "/friendship/accept",
      decline: "/friendship/decline",
      block: "/friendship/block",
      unblock: "/friendship/unblock",
      delete: "/friendship/{userId}"
    });
  }

  public getFriendship(userId1: string, userId2: string) {
    return this.wrap<IFriendshipDto>(this.httpClient.get(this.getRoute("getFriendship", { userId1, userId2 })));
  }

  public getFriendsList(userId: string) {
    return this.wrap<IUserDto[]>(this.httpClient.get(this.getRoute("getFriendsList", { userId })));
  }

  public getFriendshipInvitations() {
    return this.wrap<IUserDto[]>(this.httpClient.get(this.getRoute("getFriendshipInvitations")));
  }

  public getBlockedUsers() {
    return this.wrap<IUserDto[]>(this.httpClient.get(this.getRoute("getBlockedUsers")));
  }

  public inviteUser(userId: string) {
    return this.wrap<undefined>(this.httpClient.post(this.getRoute("invite"), { invitedUserId: userId }));
  }

  public acceptInvitation(userId: string) {
    return this.wrap<undefined>(this.httpClient.post(this.getRoute("accept"), { invitingUserId: userId }));
  }

  public declineInvitation(userId: string) {
    return this.wrap<undefined>(this.httpClient.post(this.getRoute("decline"), { invitingUserId: userId }));
  }

  public blockUser(userId: string) {
    return this.wrap<undefined>(this.httpClient.post(this.getRoute("block"), { blockedUserId: userId }));
  }

  public unblockUser(userId: string) {
    return this.wrap<undefined>(this.httpClient.post(this.getRoute("unblock"), { blockedUserId: userId }));
  }

  public deleteFriend(userId: string) {
    return this.wrap<undefined>(this.httpClient.delete(this.getRoute("delete", { userId })));
  }
}
