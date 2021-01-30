import { AsyncResult, Result } from "@/modules/common/models/Result";
import { ErrorResponse } from "@/modules/common/models/ErrorResponse";
import { HttpClient } from "@/modules/common/services/HttpClient";
import { Service } from "@/modules/common/services/Service";

interface IUserDetailsResponse {
  userId: string;
  steamId: string | null;
  username: string;
}

export class UserService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      getUserDetails: "/user/{userId}"
    });
  }

  public async getUserDetails(userId: string): AsyncResult<IUserDetailsResponse, ErrorResponse>
  {
    try {
      const res = await this.httpClient.get(this.getRoute("getUserDetails", { userId }));

      return Result.success(res.data);
    } catch (error) {
      return Result.fail(ErrorResponse.create(error.response.data));
    }
  }
}
