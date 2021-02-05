import { HttpClient } from "@/modules/common/services/HttpClient";
import { Service } from "@/modules/common/services/Service";

enum MatchStatus {
  Created,
  Starting,
  Started,
  Canceled,
  Finished
}

export interface IMatchDto {
  id: string;
  map: string;
  status: MatchStatus;
  address: string;
}

export interface IMatchHistoryDto {
  id: string;
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
  kills: number;
  assists: number;
  deaths: number;
  score: number;
}

export class MatchService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      browse: "/match",
      getById: "/match/{id}"
    });
  }

  public browse(userId: string) {
    return this.wrap<IMatchHistoryDto[]>(this.httpClient.get(this.getRoute("browse"), { params: { userId }}));
  }

  public getById(id: string) {
    return this.wrap<IMatchDto>(this.httpClient.get(this.getRoute("getById", { id })));
  }
}
