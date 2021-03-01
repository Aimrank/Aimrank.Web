
import { HttpClient } from "@/common/services/HttpClient";
import { Service } from "@/common/services/Service";
import { IUserStatsDto } from "@/match/models/IUserStatsDto";

export class StatsService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      getStats: "/user/{userId}/stats"
    });
  }

  public getStats(userId: string) {
    return this.wrap<IUserStatsDto>(this.httpClient.get(this.getRoute("getStats", { userId })));
  }
}
