import { IPaginationResponse } from "@/common/models/IPaginationResponse";
import { HttpClient } from "@/common/services/HttpClient";
import { Service } from "@/common/services/Service";
import { MatchMode } from "@/match/models/MatchMode";
import { IMatchDto } from "@/match/models/IMatchDto";
import { IMatchHistoryDto } from "@/match/models/IMatchHistoryDto";

export interface IBrowseMatchesQuery {
  page?: number;
  size?: number;
  map?: string;
  mode?: MatchMode;
}

export class MatchService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      browse: "/user/{userId}/matches",
      accept: "/match/{matchId}/accept",
      getByLobbyId: "/lobby/{lobbyId}/match"
    });
  }

  public browse(userId: string, query?: IBrowseMatchesQuery) {
    return this.wrap<IPaginationResponse<IMatchHistoryDto>>(
      this.httpClient.get(this.getRoute("browse", { userId }), { params: query })
    );
  }

  public getByLobbyId(lobbyId: string) {
    return this.wrap<IMatchDto | undefined>(this.httpClient.get(this.getRoute("getByLobbyId", { lobbyId })));
  }

  public accept(matchId: string) {
    return this.wrap<undefined>(this.httpClient.post(this.getRoute("accept", { matchId })));
  }
}
