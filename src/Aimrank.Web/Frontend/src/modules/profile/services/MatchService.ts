import { IPaginationResponse } from "@/common/models/IPaginationResponse";
import { HttpClient } from "@/common/services/HttpClient";
import { Service } from "@/common/services/Service";
import { MatchMode } from "@/profile/models/MatchMode";
import { IMatchDto } from "@/profile/models/IMatchDto";

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
    });
  }

  public browse(userId: string, query?: IBrowseMatchesQuery) {
    return this.wrap<IPaginationResponse<IMatchDto>>(
      this.httpClient.get(this.getRoute("browse", { userId }), { params: query })
    );
  }

  public accept(matchId: string) {
    return this.wrap<undefined>(this.httpClient.post(this.getRoute("accept", { matchId })));
  }
}
