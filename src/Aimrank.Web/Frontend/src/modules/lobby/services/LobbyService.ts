import { HttpClient } from "@/modules/common/services/HttpClient";
import { Service } from "@/modules/common/services/Service";

enum LobbyStatus {
  Open,
  Closed,
  InGame
}

export interface ILobbyDto {
  id: string;
  matchId: string;
  map: string;
  status: LobbyStatus;
  members: {
    userId: string;
    isLeader: boolean;
  }[];
}

export interface IChangeLobbyMapRequest {
  name: string;
}

export class LobbyService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      getList: "/lobby",
      getById: "/lobby/{id}",
      getByUserId: "/lobby/user/{id}",
      create: "/lobby",
      join: "/lobby/{id}/members",
      leave: "/lobby/{id}/members",
      changeMap: "/lobby/{id}/map",
      close: "/lobby/{id}/close"
    });
  }

  public getList() {
    return this.wrap<ILobbyDto[]>(this.httpClient.get(this.getRoute("getList")));
  }

  public getById(id: string) {
    return this.wrap<ILobbyDto>(this.httpClient.get(this.getRoute("getById", { id })));
  }

  public getByUserId(id: string) {
    return this.wrap<ILobbyDto>(this.httpClient.get(this.getRoute("getByUserId", { id })));
  }

  public create() {
    return this.wrap<void>(this.httpClient.post(this.getRoute("create")));
  }

  public join(id: string) {
    return this.wrap<void>(this.httpClient.post(this.getRoute("join", { id })));
  }

  public leave(id: string) {
    return this.wrap<void>(this.httpClient.delete(this.getRoute("leave", { id })));
  }

  public changeMap(id: string, request: IChangeLobbyMapRequest) {
    return this.wrap<void>(this.httpClient.post(this.getRoute("changeMap", { id }), request));
  }

  public close(id: string) {
    return this.wrap<void>(this.httpClient.post(this.getRoute("close", { id })));
  }
}
