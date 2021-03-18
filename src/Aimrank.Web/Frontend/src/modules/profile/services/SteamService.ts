import { httpClient } from "~/services";
import { AsyncResult, Result } from "@/common/models/Result";
import { ErrorResponse } from "@/common/models/ErrorResponse";
import { HttpClient } from "@/common/services/HttpClient";
import { Service } from "@/common/services/Service";

export class SteamService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      signInWithSteam: "/api/steam/openid"
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
