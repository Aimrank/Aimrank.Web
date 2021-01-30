import { AsyncResult, Result } from "@/modules/common/models/Result";
import { ErrorResponse } from "@/modules/common/models/ErrorResponse";
import { HttpClient } from "@/modules/common/services/HttpClient";
import { Service } from "@/modules/common/services/Service";
import { httpClient } from "@/services";

export class SteamService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      signInWithSteam: "/steam/openid"
    });
  }

  public async signInWithSteam(): AsyncResult<void, ErrorResponse> {
    try {
      const res = await httpClient.post(this.getRoute("signInWithSteam"));

      window.location.href = res.data.location;

      return Result.success();
    } catch (error) {
      return Result.fail(error.response.data);
    }
  }
}
