import { HttpClient } from "@/modules/common/services/HttpClient";
import { Service } from "@/modules/common/services/Service";

export enum MatchStatus {
  Created,
  Ready,
  Canceled,
  Starting,
  Started,
  Finished
}

export enum MatchMode {
  OneVsOne,
  TwoVsTwo
}

export enum MatchTeam {
  T = 2,
  CT = 3
}

export interface IMatchDto {
  id: string;
  map: string;
  status: MatchStatus;
  address: string;
}

export interface IMatchHistoryDto {
  id: string;
  mode: MatchMode;
  scoreT: number;
  scoreCT: number;
  createdAt: string;
  finishedAt: string;
  map: string;
  teamTerrorists: IMatchHistoryPlayerDto[];
  teamCounterTerrorists: IMatchHistoryPlayerDto[];
}

export interface IMatchHistoryPlayerDto {
  id: string;
  username: string;
  team: MatchTeam;
  kills: number;
  assists: number;
  deaths: number;
  score: number;
}

export class MatchService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      browse: "/match",
      getByLobbyId: "/lobby/{id}/match"
    });
  }

  public browse(userId: string) {
    return this.wrap<IMatchHistoryDto[]>(this.httpClient.get(this.getRoute("browse"), { params: { userId }}));
  }

  public getByLobbyId(id: string) {
    return this.wrap<IMatchDto | undefined>(this.httpClient.get(this.getRoute("getByLobbyId", { id })));
  }
}
