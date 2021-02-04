import { HttpClient } from "@/modules/common/services/HttpClient";
import { Service } from "@/modules/common/services/Service";

enum MatchStatus {
  Created,
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

export class MatchService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      getById: "/match/{id}"
    });
  }

  public getById(id: string) {
    return this.wrap<IMatchDto>(this.httpClient.get(this.getRoute("getById", { id })));
  }
}
