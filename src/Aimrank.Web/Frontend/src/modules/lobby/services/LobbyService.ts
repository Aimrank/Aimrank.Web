import { HttpClient } from "@/common/services/HttpClient";
import { Service } from "@/common/services/Service";
import { MatchMode } from "@/match/models/MatchMode";
import { ILobbyMatchDto } from "@/lobby/models/ILobbyMatchDto";
import { ILobbyDto } from "@/lobby/models/ILobbyDto";
import { ILobbyInvitationDto } from "@/lobby/models/ILobbyInvitationDto";

export interface IInviteUserToLobbyRequest {
  invitedUserId: string;
}

export interface IChangeLobbyMapRequest {
  name: string;
}

export interface IChangeLobbyConfigurationRequest {
  name: string;
  map: string;
  mode: MatchMode;
}

export class LobbyService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      getForCurrentUser: "/lobby/current",
      getInvitations: "/lobby/invitations",
      getMatch: "/lobby/{id}/match",
      create: "/lobby",
      invite: "/lobby/{id}/invite",
      inviteAccept: "/lobby/{id}/invite/accept",
      inviteCancel: "/lobby/{id}/invite/cancel",
      leave: "/lobby/{id}/members",
      changeConfiguration: "/lobby/{id}/configuration",
      startSearching: "/lobby/{id}/start",
    });
  }

  public getForCurrentUser() {
    return this.wrap<ILobbyDto | undefined>(this.httpClient.get(this.getRoute("getForCurrentUser")));
  }

  public getInvitations() {
    return this.wrap<ILobbyInvitationDto[]>(this.httpClient.get(this.getRoute("getInvitations")));
  }

  public getMatch(id: string) {
    return this.wrap<ILobbyMatchDto | undefined>(this.httpClient.get(this.getRoute("getMatch", { id })));
  }

  public create() {
    return this.wrap<void>(this.httpClient.post(this.getRoute("create")));
  }

  public invite(id: string, request: IInviteUserToLobbyRequest) {
    return this.wrap<void>(this.httpClient.post(this.getRoute("invite", { id }), request));
  }

  public acceptInvitation(id: string) {
    return this.wrap<void>(this.httpClient.post(this.getRoute("inviteAccept", { id })));
  }

  public cancelInvitation(id: string) {
    return this.wrap<void>(this.httpClient.post(this.getRoute("inviteCancel", { id })));
  }

  public leave(id: string) {
    return this.wrap<void>(this.httpClient.delete(this.getRoute("leave", { id })));
  }

  public changeConfiguration(id: string, request: IChangeLobbyConfigurationRequest) {
    return this.wrap<void>(this.httpClient.post(this.getRoute("changeConfiguration", { id }), request));
  }

  public startSearching(id: string) {
    return this.wrap<void>(this.httpClient.post(this.getRoute("startSearching", { id })));
  }
}
